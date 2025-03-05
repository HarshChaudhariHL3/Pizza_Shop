using System.Runtime.Serialization.Formatters;
using Microsoft.AspNetCore.Http;

namespace PizzaShop.Entity.ViewModel;

public class ProfileViewModel
{

    public int Id { get; set; }
    public string? Email { get; set; }

    public string? Role { get; set; }

    public string? UserName { get; set; }


    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Username { get; set; }

    public int? Country { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public string? Phone { get; set; }

    public bool Status { get; set; }

    public string? Address { get; set; }

    public int? ZipCode { get; set; }

    public IFormFile FormFile { get; set; }



    public static void Add(string v, string? userId)
    {
        throw new NotImplementedException();
    }
}
