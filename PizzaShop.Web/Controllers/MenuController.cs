using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Web.Controllers;

public class MenuController :Controller
{
    public IActionResult Menuitem()
    {
        return View();
    }
}
