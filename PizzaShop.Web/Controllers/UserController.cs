using System;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class UserController(IUserService _userService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        var UserId = Request.Cookies["id"];
        var userIdInt = int.Parse(UserId);
        var user = _userService.GetUser(userIdInt);
        return View(user);
    }

    public IActionResult GetCountries()
    {
        var countries = _userService.GetCountries();
        return Json(countries);
    }

    public IActionResult GetStates(int CountryId)
    {
        var states = _userService.GetStates(CountryId);
        return Json(states);
    }

    public IActionResult GetCities(int StateId)
    {
        var cities = _userService.GetCities(StateId);
        return Json(cities);
    }

    public async Task<IActionResult> userlist()
    {
        return View();
    }
}
