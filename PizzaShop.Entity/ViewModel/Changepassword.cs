using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Entity.ViewModel;

public class Changepassword
{

public string email { get; set; }

        [Display(Name = "OldPassword")]
    public string? Password { get; set; }

    [Display(Name = "NewPassword")]
    public string? NewPassword { get; set; }

    [Display(Name = "ConfirmPassword")]
    public string? ConfirmPassword { get; set; }
}
