namespace PizzaShop.Entity.ViewModel;

public class CategoryListViewModel
{
    public int CategoryItemId { get; set; }
    public int ModifierGroupId { get; set; }

    public int? CategoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }
    public bool DefaultTax { get; set; }

    public int? UnitId { get; set; }


    public int? Quantity { get; set; }

    public float? Price { get; set; }

    public bool ItemType { get; set; }
    public bool IsAvailable { get; set; }

    public string? ShortCode { get; set; }

    public string? ImageUrl { get; set; }

    public List<CategoryModifierMappingsViewModel> SelectedModifiers { get; set; }




}

public class CategoryModifierMappingsViewModel
{

    public int? CategoryItemId { get; set; }
    public string? ModifierGroupName { get; set; }
    public int? ModifierId { get; set; }
    public int? MaxValue { get; set; }
    public int? MinValue { get; set; }
}