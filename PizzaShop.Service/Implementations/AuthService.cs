using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class AuthService(IAuthRepository _repository) : IAuthService
{
    public async Task<User> AuthenticateUser(string email , string password)
    {

        // var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        var user = await _repository.AuthenticateUser(email, password);
        // if (user == null || !PasswordUtills.VerifyPassword(password, user.PasswordHash))
        //     return null;
        if ( user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            return null ;
        }
        return user;
    }

    public async Task<User> Useremail(string email)
    {

        // var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        var user = await _repository.Useremail(email);
        // if (user == null || !PasswordUtills.VerifyPassword(password, user.PasswordHash))
        //     return null;
        if ( user == null)
        {
            return null ;
        }
        return user;
    }

    public async Task<(bool success, string error)> ChangePassword(Resetpassword model){
        if(model.NewPassword != model.ConfirmPassword){
            return (false, "Password does not match");
        }
        var user = await _repository.Useremail(model.email);
        if(user == null){
            return (false, "User not found");
        }

        var hashed = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        user.Password = hashed ;
        bool success = await _repository.Savepassword(user);
        return (success , success ? null : "Failed to update Password");

    }
    public async Task<(bool success, string error)> ChangePassword(Changepassword model){
        if(model.NewPassword != model.ConfirmPassword){
            return (false, "Password does not match");
        }
        var user = await _repository.Useremail(model.email);
        if(user == null){
            return (false, "User not found");
        }

        var hashed = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
        user.Password = hashed ;
        bool success = await _repository.Savepassword(user);
        return (success , success ? null : "Failed to update Password");

    }

    

    public async Task<Role?> CheckRole(string role)
    {
        var rolename = await _repository.GetRole(role);
        return rolename;
    }


}
