using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Entity.ViewModel;

public class Login
{
    [Required(ErrorMessage = "The email address is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    // [RegularExpression]
    public required string email { get; set; }
    public required string password { get; set; }

    public bool Rememberme { get; set; }




}
