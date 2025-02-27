using System.Security.Claims;

namespace PizzaShop.Service.Interfaces;

public interface IJwtService
{
         string GenerateJwtToken(string name, string email, int role);
        ClaimsPrincipal? ValidateToken(string token);
}
