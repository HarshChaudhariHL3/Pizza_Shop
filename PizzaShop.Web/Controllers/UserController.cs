using System;
using System.Diagnostics;
using AuthenticationDemo.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Implementations;
using PizzaShop.Service.Interfaces;
using PizzaShop.Service.Utils;
using PizzaShop.Web.Models;

namespace PizzaShop.Web.Controllers;

public class UserController(IUserService _userService, IJwtService _jwtService, IPaginationService _paginationService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Profile()
    {
        try
        {
            // var principal = _jwtService.ValidateToken(Request.Cookies["SuperSecretAuthToken"]);
            // if (principal == null)
            // {
            //     return RedirectToAction("Login", "Validation");
            // }
            // int UserId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");
            var User = SessionUtils.GetUser(HttpContext);
            var UserId = User.UserId;
            if (UserId == 0)
            {
                return BadRequest("User ID is missing.");
            }
            var user = _userService.GetUser(UserId.ToString());
            return View(user);

        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }

    }

    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel model)
    {
        try
        {
            if (model.FormFile != null && model.FormFile.Length > 0)
            {
                model.Imgurl = await ProfileImageUploadUtils.SaveProfileImageUploadAsync(model.FormFile);
            }


            // var principal = _jwtService.ValidateToken(Request.Cookies["SuperSecretAuthToken"]);
            // if (principal == null)
            // {
            //     return RedirectToAction("Login", "Validation");
            // }
            // int UserId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");

            var User = SessionUtils.GetUser(HttpContext);
            var UserId = User.UserId;


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
            user.Status = model.Status;
            user.Imgurl = model.Imgurl;
            _userService.UpdateUser(user);
            TempData["Success"] = "Profile Updated Successfully";
            return RedirectToAction("Profile");


            // user.ProfileImg = model.ProfileImg;
            // user.MyImage = model.MyImage;
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public ActionResult GetCountries()
    {
        try
        {
            var countries = _userService.GetCountries();
            return Json(countries);
        }
        catch (Exception ex)
        {
           TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult GetRoles()
    {
        try
        {
            var roles = _userService.GetRoles();
            return Json(roles);
        }
        catch (Exception ex)
        {
           TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult GetStates(int CountryId)
    {
        try
        {
            var states = _userService.GetStates(CountryId);
            return Json(states);
        }
        catch (Exception ex)
        {
           TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult GetCities(int StateId)
    {
        try
        {
            var cities = _userService.GetCities(StateId);
            return Json(cities);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpGet]
    public IActionResult Users(int pagesize = 3, int page = 1)
    {
        try
        {
            string search = null;
            var data = _paginationService.pagination_user_list(page, pagesize, search);
            return View(data);
            
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult Users(int pagesize = 3, int page = 1, string search = null)
    {
        try
        {
            var data = _paginationService.pagination_user_list(page, pagesize, search);
            if (data == null)
            {
                TempData["Error"] = "Not Valid User";
                return RedirectToAction("Users", "User");
            }
            // ViewBag.users = data.Items;
            // ViewBag.pagesize = pagesize;
            // ViewBag.page = page;
            // ViewBag.totalpages = data.TotalPages;
            return View(data);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddUser(AdduserViewModel model)
    {
        try
        {
            _userService.AddUser(model);

            TempData["Success"] = "User Added Successfully";
            return RedirectToAction("Users");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString()); ;
        }
    }

    [HttpGet]
    public IActionResult EditUser(string Email)
    {
        try
        {
            if (Email == null)
            {
                TempData["Error"] = "Email not Found";
                return View();
            }
            var user = _userService.GetUserByEmail(Email);

            return View(user);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult EditUser(EdituserViewModel model)
    {
        try
        {
            var user = _userService.GetUserByEmail(model.Email);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Username = model.Username;
            user.Status = model.Status;
            user.Country = model.Country;
            user.State = model.State;
            user.City = model.City;
            user.Phone = model.Phone;
            user.Address = model.Address;
            user.ZipCode = model.ZipCode;
            _userService.UpdateUserinEdit(user);
            TempData["Success"] = "User Updated Successfully";
            return RedirectToAction("Users");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteUser(string Email)
    {
        try
        {
            _userService.DeleteUser(Email);
            TempData["Success"] = "User Deleted Successfully";
            return RedirectToAction("Users");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
        
    }

}
