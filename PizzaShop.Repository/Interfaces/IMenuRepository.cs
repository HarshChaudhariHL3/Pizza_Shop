using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    List<Category> CategoryList();

    Category getCategoryById(int id);

    void AddCategory(MenuViewModel model);

    void DeleteCategory(int id);
   MenuViewModel EditCategory(Category category);
}
