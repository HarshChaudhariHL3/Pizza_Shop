namespace PizzaShop.Entity.ViewModel;

public class KotViewModel
{
    public List<KotCategory> CategoryList { get; set; }

    public List<KotOrderDetails> OrderDetails { get; set; }

}


public class KotCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

}

public class KotOrderDetails
{
    public int OrderId { get; set; }

    public string OrderType { get; set; } = null!;

    public string Status { get; set; } = null!;

    public float TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string TableName { get; set; } = null!;

}

