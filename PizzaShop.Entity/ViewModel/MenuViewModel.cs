namespace PizzaShop.Entity.ViewModel;

public class MenuViewModel
{
    public int CategoryId {get; set;}
    public string CategoryName {get; set;}
    public string Description {get; set;}

    public List<CategoryViewModel> CategoriesList {get; set;}
}
