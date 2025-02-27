using System.Runtime.Serialization.Formatters;

namespace PizzaShop.Entity.ViewModel;

public class ProfileViewModel
{
    public string? Email { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Username { get; set; }

    public int? Country { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public int? Phone { get; set; }

    public string? Address { get; set; }

    public int? ZipCode { get; set; }

     public string ProfileImg { get; set; }

     public IFieldInfo? MyImage { get; set; }

}
