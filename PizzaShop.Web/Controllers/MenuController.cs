using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class MenuController(IMenuService _menuService) : Controller
{
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
                }).ToList()
            };
            return View(menu);
        }
        return null;

    }

    [HttpPost]
    public IActionResult AddCategory(MenuViewModel model)
    {
        _menuService.AddCategory(model);
        TempData["Success"] = "Added Category Successfully";
        return RedirectToAction("Menu");
    }

    [HttpPost]
    public IActionResult DeleteCategory(int catogryid)
    {
        _menuService.DeleteCategory(catogryid);
        TempData["Success"] = "Deleted Successfully";
        return RedirectToAction("Menu");
    }

    [HttpPost]
    public IActionResult EditCategory(MenuViewModel model)
    {
        _menuService.EditCategory(model);
        TempData["Success"] = "Edited Successfully";
        return RedirectToAction("Menu");
    }
}
