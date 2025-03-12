using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();
     List<Unit> UnitList();

    List<CategoryListViewModel> GetCategoryItemsByCategoryId(int categoryId);

    void AddCategory(MenuViewModel model);

    CategoryListViewModel GetItemById(int itemId);

    MenuViewModel GetCategoryById(int id);

    void EditCategory(MenuViewModel model);

    void RemoveCategory(int id);

    bool DeleteCategoryItem(int itemId);

    void EditCategoryItem(CategoryListViewModel model);
}
