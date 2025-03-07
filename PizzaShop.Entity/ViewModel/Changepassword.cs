using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Entity.ViewModel;

public class Changepassword
{

public string email { get; set; }

    [Display(Name = "OldPassword")]
    [Required]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "NewPassword")]
    public string? NewPassword { get; set; }

    [Display(Name = "ConfirmPassword")]
    [Required]
    public string? ConfirmPassword { get; set; }
}
