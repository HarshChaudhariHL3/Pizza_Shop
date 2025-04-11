using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Entity.ViewModel;

public class WaitingListViewModel
{
    [Required]
    public string? Email {get; set;}
    [Required]
    public string? UserName {get; set;}
    [Required]
    public string? PhoneNumber {get; set;}
    [Required]
    public int? NoOfPerson {get; set;}
    [Required]
    public string? SectionName {get; set;}
    [Required]
    public int? SectionId {get; set;}
}
