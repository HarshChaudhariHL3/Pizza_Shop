using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();
    List<ModifierGroup> GetModifierGroups();
     List<Unit> UnitList();

    List<CategoryListViewModel> GetCategoryItemsByCategoryId(int categoryId);

    List<ModifierListViewModel> GetModifierItemsByModifierId(int ModifierGroupId);

    void AddCategory(MenuViewModel model);

    void AddModifier(MenuViewModel model);
    void AddCategoryItem(CategoryListViewModel model);

    void AddModifierItem(ModifierListViewModel model);

    CategoryListViewModel GetItemById(int itemId);
    ModifierListViewModel GetModifierItemById(int itemId);

    MenuViewModel GetCategoryById(int id);

    void EditCategory(MenuViewModel model);

    void EditModifier(MenuViewModel model);

    void RemoveCategory(int id);

    void RemoveModifier(int id);

    bool DeleteCategoryItem(int itemId);
    bool DeleteModifierItem(int itemId);

    void EditCategoryItem(CategoryListViewModel model);
    void EditModifierItem(ModifierListViewModel model);
}
