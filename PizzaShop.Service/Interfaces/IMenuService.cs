using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();

    List<CategoryItem> GetCategoryItems();

    void AddCategory(MenuViewModel model);

    MenuViewModel GetCategoryById(int id);

    void EditCategory(MenuViewModel model);

    void RemoveCategory(int id);

}
