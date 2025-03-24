using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    List<Category> CategoryList();

    List<ModifierGroup> ModifierList();

     List<Unit> UnitList();
    CategoryItem GetItemById(int itemId);

    ModifierItem GetModifierItemById(int itemId);

    void DeleteCategoryItem(int itemId);

    void DeleteModifierItem(int itemId);

     void AddCategoryItem(CategoryListViewModel model);
     ModifierItem  AddModifierItem(ModifierListViewModel model);

     public MappingItemModifier GetMappingModifierItemById(int itemId);

    List<CategoryItem> CategoryItemList(int categoryId);

    Task<List<ModifierItem>> ModifierItemList(int ModifierGroupId);
    // List<ModifierItem> ModifierItemListByModifierGroupId(int ModifierGroupId);

    void AddCategory(MenuViewModel model);

    ModifierGroup AddModifier(ModifierGroup modifier);
    MappingItemModifier AddModifierMapping(MappingItemModifier modifier);

    Category GetCategoryById(int id);

    ModifierGroup GetModifierGroupById(int id);
    void UpdateCategory(Category category);

    void UpdateModifier(ModifierGroup modifier);

    void DeleteCategory(int id);

    void DeleteModifier(int id);

    void UpdateCategoryItem(CategoryItem categoryItem);

    void UpdateModifierItem(ModifierItem modifierItem);
    CategoryItem GetCategoryItemById(int id);
    List<ModifierItem> ModifierItemsList();

    void  DeleteMultipleCategoryItem(List<int> dataId);
    void  DeleteMultipleModifierItem(List<int> dataId);
}
