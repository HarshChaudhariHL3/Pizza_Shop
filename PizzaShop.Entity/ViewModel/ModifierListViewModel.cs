namespace PizzaShop.Entity.ViewModel;

public class ModifierListViewModel
{
    public int ModifierItemId { get; set; }

    public int ModifierGroupId { get; set; }
    public int MaxValue { get; set; }
    public int MinValue { get; set; }

    public string ModifierItemName { get; set; }
    public string ModifierGroupName { get; set; }
    public float? Rate { get; set; }

    public int? UnitId { get; set; }
    public string? UnitName { get; set; }

    // public string? ImageUrl { get; set; }

    public int? Quantity { get; set; }

    public string? Description { get; set; }


}
