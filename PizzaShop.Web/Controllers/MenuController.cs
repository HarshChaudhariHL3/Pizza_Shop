using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class MenuController(IMenuService _menuService) : Controller
{
    [HttpGet]

    [HttpGet]
    public IActionResult Menu()
    {
        var data = _menuService.GetCategories();

        if (data != null)
        {
            var menu = new MenuViewModel
            {
                CategoriesList = data.Select(p => new CategoryViewModel
                {
                    CategoryId = p.CategoryId,
                    CategoryName = p.CategoryName,
                    Description = p.Description
                }).ToList(),
            };
            return View(menu);
        }
        return View(new MenuViewModel());
    }
    [HttpGet]
    public IActionResult CategoryItems(int categoryId)
    {
        var categoryItems = _menuService.GetCategoryItemsByCategoryId(categoryId);

        return Json(categoryItems);
    }

    [HttpPost]
    public IActionResult AddCategory(MenuViewModel model)
    {
        _menuService.AddCategory(model);
        TempData["Success"] = "Added Category Successfully";
        return RedirectToAction("Menu");
    }

    [HttpPost]

    public IActionResult EditCategory(MenuViewModel model)
    {
        _menuService.EditCategory(model);
        TempData["Success"] = "Category Updated successfully.";
        return RedirectToAction("Menu");
    }

    [HttpPost]
    public IActionResult DeleteCategory(MenuViewModel model)
    {
        _menuService.RemoveCategory(model.CategoryId);
        TempData["Success"] = "Deleted successfully.";
        return RedirectToAction("Menu");
    }

    public IActionResult GetCategory()
    {
        var category = _menuService.GetCategories();
        return Json(category);
    }
}
