using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    List<Category> CategoryList();

    List<CategoryItem> CategoryItemList(int categoryId);

    void AddCategory(MenuViewModel model);

    Category GetCategoryById(int id);
    void UpdateCategory(Category category);

    void DeleteCategory(int id);
}
