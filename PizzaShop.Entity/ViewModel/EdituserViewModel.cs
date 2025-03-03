using System.Runtime.Serialization.Formatters;

namespace PizzaShop.Entity.ViewModel;

public class EdituserViewModel
{
    public string? Email { get; set; }

    public int? Role { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? Username { get; set; }

    public bool? Status { get; set; }

    public int? Country { get; set; }

    public int? State { get; set; }

    public int? City { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? ZipCode { get; set; }

     public IFieldInfo? ProfileImg { get; set; }
}
