using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Web.Models;

namespace PizzaShop.Web.Controllers;

public class ErrorController : Controller
{
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
    }
}
