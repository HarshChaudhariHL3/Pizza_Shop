using Microsoft.AspNetCore.Mvc;
using PizzaShop.Service.Interfaces;


namespace PizzaShop.Web.Controllers;

public class OrderAppController(IOrderAppService _orderAppService) : Controller
{
    public IActionResult Kot()
    {
        var CategoryList = _orderAppService.GetCategoryList();
        return View(CategoryList);
    }





    public IActionResult MenuOrderApp()
    {
        return View();
    }
    public IActionResult Tables()
    {
        return View();
    }
    public IActionResult WaitingList()
    {
        return View();
    }

}
