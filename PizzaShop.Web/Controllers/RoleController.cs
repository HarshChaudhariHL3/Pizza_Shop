using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class RoleController(IUserService _service) : Controller
{
    public IActionResult Roles()
    {
        ViewBag.Roles = _service.GetRoles();
        return View();
    }

        public IActionResult Permission(string role)
    {

        ViewBag.Role = role;
        return View();
    }
}
