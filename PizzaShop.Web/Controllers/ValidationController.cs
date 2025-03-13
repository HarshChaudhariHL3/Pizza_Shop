using System.Diagnostics;
using System.Threading.Tasks;
using AuthenticationDemo.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;
using PizzaShop.Web.Models;


namespace PizzaShop.Web.Controllers;

[AllowAnonymous]
public class ValidationController(IAuthService _authService, IJwtService _jwtService, ISendmailService _sendmail) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        try
        {
            var user = SessionUtils.GetUser(HttpContext);
            if (user == null)
                return View();
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login(Login model)
    {
        try
        {
            var user = await _authService.AuthenticateUser(model.email, model.password);
            if (user == null)
            {
                TempData["Error"] = "Invalid email or password";
                return View(model);
            }

            var token = _jwtService.GenerateJwtToken(user.FirstName + " " + user.LastName, user.Email, user.UserRole ?? 0, user.UserId);
            CookieUtils.SaveJWTToken(Response, token);

            if (model.Rememberme)
            {
                CookieUtils.SaveUserData(Response, user);
            }

            SessionUtils.SetUser(HttpContext, user);

            TempData["Success"] = "Login Successfull";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    public IActionResult Forgotpassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Forgotpassword(Forgotpassword model)
    {
        try
        {
            if (string.IsNullOrEmpty(model.email))
            {
                TempData["Error"] = "Email is Required";
                return View(model);
            }

            var user = _authService.Useremail(model.email);
            if (user == null)
            {
                TempData["Error"] = "Email not Found";
                return View(model);
            }

            var resetLink = Url.Action("Resetpassword", "Validation", new { user_Email = model.email }, Request.Scheme);

            Console.WriteLine(resetLink);
            Task task = _sendmail.SendMail(model.email, resetLink);


            TempData["Success"] = "Email has been sent to reset your password";
            return RedirectToAction("Login", "Validation");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpGet]
    public IActionResult Resetpassword(string user_Email)
    {
        try
        {
            Resetpassword change = new();
            change.email = user_Email;
            return View(change);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Resetpassword(Resetpassword model)
    {
        try
        {
            var (success, error) = await _authService.ChangePassword(model);

            if (error != null)
            {
                TempData["Error"] = error ?? "Invalid";
                return RedirectToAction("ResetPassword", "Validation");
            }
            if (success)
            {
                TempData["Success"] = "Password is Change Successfully";
                return RedirectToAction("Login", "Validation");
            }

            return RedirectToAction("ResetPassword", "Validation");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult Changepassword()
    {
        try
        {
            var useremail = SessionUtils.GetUser(HttpContext);
            Changepassword change = new();
            change.email = useremail?.Email ?? string.Empty;
            return View(change);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Changepassword(Changepassword model)
    {
        try
        {
            var (success, error) = await _authService.ChangePassword(model);

            if (error != null)
            {
                TempData["Error"] = error ?? "Invalid";
                return RedirectToAction("ResetPassword", "Validation");
            }
            if (success)
            {
                TempData["Success"] = "Password is Change Successfully";
                return RedirectToAction("Login", "Validation");
            }

            return RedirectToAction("ResetPassword", "Validation");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult Logout()
    {
        try
        {
            CookieUtils.ClearCookies(HttpContext);
            SessionUtils.ClearSession(HttpContext);

            return RedirectToAction("Login");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

