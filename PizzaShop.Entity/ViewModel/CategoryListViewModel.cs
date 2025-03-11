namespace PizzaShop.Entity.ViewModel;

public class CategoryListViewModel
{
    public int CategoryItemId { get; set; }

    public int? CategoryId { get; set; }

    public string ItemName { get; set; } = null!;

    public string? Description { get; set; }

    public int? Quantity { get; set; }

    public float? Price { get; set; }

    public bool? IsAvailable { get; set; }

    public string? ShortCode { get; set; }

    public string? ImageUrl { get; set; }


}
