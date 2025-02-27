using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IAuthService
{
    Task<User> AuthenticateUser(string email, string password);

    Task<User> Useremail(string email);

    Task<(bool success, string error)> ChangePassword(Changepassword model);

    Task<Role?> CheckRole(string role);

}
