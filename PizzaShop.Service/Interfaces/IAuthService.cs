using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IAuthService
{
    Task<User> AuthenticateUser(string email, string password);

    Task<User> Useremail(string email);
    Task<Role?> CheckRole(string role);
    Task<(bool success, string? error)> ChangePassword(Resetpassword model);
    Task<(bool success, string? error)> ChangePassword(Changepassword model);

}
