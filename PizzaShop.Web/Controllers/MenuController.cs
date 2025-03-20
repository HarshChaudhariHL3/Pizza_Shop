using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class MenuController(IMenuService _menuService, IPaginationService _paginationService) : Controller
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
            return PartialView("./PartialView/MenuController");
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

    [HttpGet]
    public IActionResult CategoryListPagination(int pagesize = 3, int page = 1)
    {
        try
        {
            string search = null;
            var data = _paginationService.pagination_category_list(page, pagesize, search);
            return View(data);

        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult CategoryListPagination(int pagesize = 3, int page = 1, string search = null)
    {
        try
        {
            var data = _paginationService.pagination_category_list(page, pagesize, search);
            if (data == null)
            {
                TempData["Error"] = "Not Valid User";
                return RedirectToAction("Users", "User");
            }
            // ViewBag.users = data.Items;
            // ViewBag.pagesize = pagesize;
            // ViewBag.page = page;
            // ViewBag.totalpages = data.TotalPages;
            return View(data);
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

            // var modifierItems = _menuService.GetModifierItemsByModifierId(ModifierGroupId);

            // return Json(modifierItems);
            return PartialView("./PartialView/ModifierList", modifierItems);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpGet]
    public IActionResult GetModifierItemDetails(int itemId)
    {
        try
        {
            var item = _menuService.GetModifierItemById(itemId);
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
    public IActionResult AddModifier(MenuViewModel model)
    {
        try
        {
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

    [HttpGet]
    public IActionResult GetALLModifierItemDetails()
    {
        var modifierItemsList = _menuService.GetModifierItems();
        return Json(modifierItemsList);
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
