using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Implementations;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class UserController(IUserService _userService, IJwtService _jwtService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Profile()
    {

        // var UserId = Request.Cookies["id"];
        var principal = _jwtService.ValidateToken(Request.Cookies["SuperSecretAuthToken"]);
        if (principal == null)
        {
            return RedirectToAction("Login", "Validation");
        }
        int UserId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");


        if (UserId == 0)
        {
            return BadRequest("User ID is missing.");
        }
        var user = _userService.GetUser(UserId.ToString());
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel model){

        // var UserId = Request.Cookies["id"];
        var principal = _jwtService.ValidateToken(Request.Cookies["SuperSecretAuthToken"]);
        if (principal == null)
        {
            return RedirectToAction("Login", "Validation");
        }
        int UserId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");


        if (UserId == 0)
        {
            return BadRequest("User ID is missing.");
        }
        model.Id = UserId;
        var user = _userService.GetUser(UserId.ToString());
        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.Phone = model.Phone;
        user.Address = model.Address;
        user.ZipCode = model.ZipCode;
        user.Country = model.Country;
        user.State = model.State;
        user.City = model.City;
        _userService.UpdateUser(user);
        TempData["Success"] = "Profile Updated Successfully";
        return RedirectToAction("Profile");


        // user.ProfileImg = model.ProfileImg;
        // user.MyImage = model.MyImage;
    }

    public ActionResult GetCountries()
    {
        var countries = _userService.GetCountries();
        return Json(countries);
    }

    public IActionResult GetRoles()
    {
        var roles = _userService.GetRoles();
        return Json(roles);
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

    [HttpGet]
    public IActionResult Users(int pagesize = 3 , int page = 1){
        string search = null;
        var data = _userService.pagination_user_list(page, pagesize, search);
        ViewBag.users = data.Items;
        ViewBag.pagesize = pagesize;
        ViewBag.page = page;
        ViewBag.totalpages = data.TotalPages;
        return View();
    }

    [HttpPost]
    public IActionResult Users(int pagesize = 3, int page = 1, string search = null)
    {
        var data = _userService.pagination_user_list(page, pagesize, search);
        ViewBag.users = data.Items;
        ViewBag.pagesize = pagesize;
        ViewBag.page = page;
        ViewBag.totalpages = data.TotalPages;
        return View();
    }

    [Authorize(Roles = "1")]
    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(AdduserViewModel model)
    {
        _userService.AddUser(model);

        TempData["Success"] = "User Added Successfully";
        return RedirectToAction("Users");
    }

    
}
