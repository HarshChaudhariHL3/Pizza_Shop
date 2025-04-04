using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

[ServiceFilter(typeof(PermissionFilter))]
public class MenuController(IMenuService _menuService) : Controller
{

    [HttpGet]
    public IActionResult Menu()
    {
        try
        {
            var data = _menuService.GetCategories();
            var data2 = _menuService.GetModifierGroups();

            if (data != null && data2 != null)
            {
                var menu = new MenuViewModel
                {
                    CategoriesList = data.Select(p => new CategoryViewModel
                    {
                        CategoryId = p.CategoryId,
                        CategoryName = p.CategoryName,
                        Description = p.Description
                    }).ToList(),

                    ModifierList = data2.Select(p => new ModifierViewModel
                    {
                        ModifierGroupId = p.ModifierGroupId,
                        ModifierName = p.ModifierName,
                        Description = p.Description
                    }).ToList(),

                };
                return View(menu);
            }
            return View(new MenuViewModel());
        }

        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    #region Category


    // for categoryList View
    [HttpGet]
    public async Task<IActionResult> CategoryItems(int categoryId, int page , int pageSize, string search ="")
    {
        try
        {
            PaginationViewModel<CategoryListViewModel> categoryItems = await _menuService.GetCategoryItems(categoryId,page,pageSize,search);
            // var categoryItems = _menuService.GetCategoryItemsByCategoryId(categoryId);

            // return Json(categoryItems);
            // return PartialView(categoryItems);
            return PartialView("./PartialView/CategoryList", categoryItems);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    // for edit categoryItems
    [HttpGet]
    public IActionResult GetItemDetails(int itemId)
    {
        try
        {
            var item = _menuService.GetItemById(itemId);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult UpdateCategoryItem(CategoryListViewModel model)
    {
        try
        {
            _menuService.EditCategoryItem(model);
            TempData["Success"] = "CategoryItem Edited Successfully";
            return PartialView("./PartialView/Category");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult AddCategoryItem(CategoryListViewModel model)
    {
        try
        {
             if (model.SelectedModifiers != null)
        {
            Console.WriteLine("Selected Modifiers: " + string.Join(", ", model.SelectedModifiers));
        }
            _menuService.AddCategoryItem(model);
            TempData["Success"] = "CategoryItem Added Successfully";
            return Json(new { Success = true });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult AddCategory(MenuViewModel model)
    {
        try
        {
            _menuService.AddCategory(model);
            TempData["Success"] = "Added Category Successfully";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]

    public IActionResult EditCategory(MenuViewModel model)
    {
        try
        {
            _menuService.EditCategory(model);
            TempData["Success"] = "Category Updated successfully.";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteCategory(MenuViewModel model)
    {
        try
        {
            _menuService.RemoveCategory(model.CategoryId);
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult GetCategory()
    {
        try
        {
            var category = _menuService.GetCategories();
            return Json(category);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteCategoryItem(int itemId)
    {
        try
        {
            var result = _menuService.DeleteCategoryItem(itemId);

            if (result)
            {
                TempData["Success"] = "Category item deleted successfully.";
                return Json(result);
            }
            else
            {
                TempData["Error"] = "Failed to delete category item.";
                return Json(result);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteMultipleCategoryItem(List<int> dataId)
    {
        try
        {
            if (dataId.Count != 0)
            {

                var result = _menuService.DeleteMultipleCategoryItem(dataId);

                if (result)
                {
                    TempData["Success"] = "Category item deleted successfully.";
                    return Json(result);
                }
                else
                {
                    TempData["Error"] = "Failed to delete category item.";
                    return Json(result);
                }
            }
            else
            {
                TempData["Error"] = "Please select Category Item";
                return RedirectToAction("Menu", "Menu");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
        

    }

    #endregion

    #region Modifier
    [HttpGet]
    public async Task<IActionResult> ModifierItems(int ModifierGroupId, int page , int pageSize, string search ="")
    {
        try
        {
            PaginationViewModel<ModifierListViewModel> modifierItems = await _menuService.GetModifierItems(ModifierGroupId,page,pageSize,search);

           
            return PartialView("./PartialView/ModifierList", modifierItems);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    // [Route("/Menu/GetModifierItemDetails")]
    [HttpGet]
    public async Task<IActionResult> GetModifierItemDetails(int modifierGroupId)
    {
        try
        {
            var item = await _menuService.GetModifierItemById(modifierGroupId);
                if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    [Route("/Menu/GetAllModifier")]
    
    [HttpGet]
    public IActionResult GetAllModifier()
    {
        try
        {
            var item = _menuService.GetAllModifierById();
            return Json(item);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    [HttpGet]
    public IActionResult ModifierItemsList()
    {
        try
        {
            var item = _menuService.ModifierItemsList();
            return Json(item);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }


    [HttpPost]
    public IActionResult UpdateModifierItem(ModifierListViewModel model)
    {
        try
        {
            _menuService.EditModifierItem(model);
            TempData["Success"] = "CategoryItem Edited Successfully";
            return Json(new { Success = true });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult AddModifierItem(ModifierListViewModel model)
    {
        try
        {
            _menuService.AddModifierItem(model);
            TempData["Success"] = "ModifierItem added Successfully";
            return Json(new { Success = true });
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }




    [HttpPost]
    public IActionResult AddModifier(MenuViewModel model, string SelectedModifierIds, string selectedModifierName)
    {
        try
        {
            if (!string.IsNullOrEmpty(SelectedModifierIds))
        {
            model.SelectedModifierIds = SelectedModifierIds.Split(',').Select(int.Parse).ToList();
            model.SelectedModifierName = selectedModifierName.Split(',').ToList();
        }
            _menuService.AddModifier(model);
            TempData["Success"] = "Added Modifier Successfully";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    //  public async Task<IActionResult> AddModifierGroup(AddModifierGroupViewModel model, string SelectedModifierIds, string selectedModifierName)
    // {
    //     if (!string.IsNullOrEmpty(SelectedModifierIds))
    //     {
    //         model.SelectedModifierIds = SelectedModifierIds.Split(',').Select(int.Parse).ToList();
    //         model.selectedModifierName = selectedModifierName.Split(',').ToList();
    //     }
    //     _categoryService.AddModifierGroupAsync(model);
    //     TempData["succses"] = "Add Modifier Group";
    //     return RedirectToAction("Menupage");
    // }



    [HttpPost]

    public IActionResult EditModifier(MenuViewModel model)
    {
        try
        {
            _menuService.EditModifier(model);
            TempData["Success"] = "Category Updated successfully.";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }


    [HttpPost]
    public IActionResult DeleteModifier(MenuViewModel model)
    {
        try
        {
            _menuService.RemoveModifier(model.ModifierGroupId);
            TempData["Success"] = "Deleted successfully.";
            return RedirectToAction("Menu");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }


    public IActionResult GetModifier()
    {
        try
        {
            var modifiers = _menuService.GetModifierGroups();
            return Json(modifiers);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult GetUnitList()
    {
        try
        {
            var list = _menuService.UnitList();
            return Json(list);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }


    [HttpPost]
    public IActionResult DeleteModifierItem(int ModifierItemId)
    {
        try
        {
            var result = _menuService.DeleteModifierItem(ModifierItemId);

            if (result)
            {
                TempData["Success"] = "Modifier item deleted successfully.";
                return Json(result);
            }
            else
            {
                TempData["Error"] = "Failed to delete modifier item.";
                return Json(result);
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    // [HttpGet]
    // public IActionResult GetALLModifierItemDetails()
    // {
    //     var modifierItemsList = _menuService.GetModifierItems();
    //     return PartialView("./PartialView/_ExistingModifiersPartial" ,modifierItemsList);
    // }
    [HttpGet]
    public async Task<IActionResult> GetALLModifierItemDetails( int page , int pageSize, string search ="")
    {
        try
        {
            PaginationViewModel<ExistingModifierViewModel> modifierItemsList = await _menuService.GetModifierItems(page,pageSize,search);
            return PartialView("PartialView/_ExistingModifiersPartial" ,modifierItemsList);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteMultipleModifierItem(List<int> dataId)
    {
        try
        {
            if (dataId.Count != 0)
            {

                var result = _menuService.DeleteMultipleModifierItem(dataId);

                if (result)
                {
                    TempData["Success"] = "Category item deleted successfully.";
                    return Json(result);
                }
                else
                {
                    TempData["Error"] = "Failed to delete category item.";
                    return Json(result);
                }
            }
            else
            {
                TempData["Error"] = "Please select Category Item";
                return RedirectToAction("Menu", "Menu");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }




    #endregion
}
