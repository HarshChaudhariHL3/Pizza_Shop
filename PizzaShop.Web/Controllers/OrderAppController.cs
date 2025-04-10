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

    // public IActionResult KotOrders(int categoryId , string progress){
    //     var orderDetails = _orderAppService.GetKOTCardDetails(categoryId, progress);
    //     return PartialView("_KotModifierItem", orderDetails);
    // }





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
