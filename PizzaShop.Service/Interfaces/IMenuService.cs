using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();
    List<ModifierGroup> GetModifierGroups();
     List<Unit> UnitList();

    Task<PaginationViewModel<CategoryListViewModel>> GetCategoryItems(int categoryId, int page , int pageSize, string search);
    // List<CategoryListViewModel> GetCategoryItemsByCategoryId(int categoryId);

    // List<ModifierListViewModel> GetModifierItemsByModifierId(int ModifierGroupId);
    Task<PaginationViewModel<ModifierListViewModel>> GetModifierItems(int ModifierGroupId, int page , int pageSize, string search ="");

    void AddCategory(MenuViewModel model);

    Task AddModifier(MenuViewModel model);
    void AddCategoryItem(CategoryListViewModel model);

    void AddModifierItem(ModifierListViewModel model);

    CategoryListViewModel GetItemById(int itemId);
    Task<List<ModifierListViewModel>> GetModifierItemById(int itemId);
    List<ModifierGroup> GetAllModifierById();
    MenuViewModel GetCategoryById(int id);

    void EditCategory(MenuViewModel model);

    void EditModifier(MenuViewModel model);

    void RemoveCategory(int id);

    void RemoveModifier(int id);

    bool DeleteCategoryItem(int itemId);
    bool DeleteMultipleCategoryItem(List<int> dataId);

    bool DeleteMultipleModifierItem(List<int> dataId);
    bool DeleteModifierItem(int itemId);

    void EditCategoryItem(CategoryListViewModel model);
    void EditModifierItem(ModifierListViewModel model);

    List<ModifierItem> GetModifierItems();

}


