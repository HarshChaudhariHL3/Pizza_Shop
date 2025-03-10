namespace PizzaShop.Entity.ViewModel;

public class MenuViewModel
{
    public int CategoryId {get; set;}
    public string CategoryName {get; set;}
    public string Description {get; set;}
    public string Category {get; set;}

    public List<CategoryViewModel> CategoriesList {get; set;}

    public List<CategoryListViewModel> ItemsList {get; set;} = new List<CategoryListViewModel>();
}
