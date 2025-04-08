using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Web.Controllers;

public class OrderAppController : Controller
{
    public IActionResult Kot()
    {
        return View();
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
