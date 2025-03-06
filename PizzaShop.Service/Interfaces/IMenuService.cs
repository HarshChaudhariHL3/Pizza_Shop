using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();

    void AddCategory(MenuViewModel model);

    void DeleteCategory(int id);

    MenuViewModel EditCategory(MenuViewModel model);
}
