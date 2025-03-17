namespace PizzaShop.Entity.ViewModel;

public class ModifierListViewModel
{
    public int ModifierItemId { get; set; }

    public int? ModifierGroupId { get; set; }

    public string ModifierItemName { get; set; } = null!;

    public float? Rate { get; set; }

    public int? UnitId { get; set; }
    public string? UnitName { get; set; }

    public string? ImageUrl { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }


}
