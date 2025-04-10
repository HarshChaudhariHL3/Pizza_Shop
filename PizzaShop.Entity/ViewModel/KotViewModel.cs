namespace PizzaShop.Entity.ViewModel;

public class KotViewModel
{
    public List<KotCategory> CategoryList { get; set; }

}


public class KotCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

}

