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

    public void AddCategory (){
        System.Console.WriteLine("addcategory");
    }
}
