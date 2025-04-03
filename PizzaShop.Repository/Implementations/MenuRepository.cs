using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class MenuRepository(PizzaShopDbContext _context) : IMenuRepository
{

    #region Category
    public List<Category> CategoryList()
    {
        var items = _context.Categories.OrderBy(c => c.CategoryName).ToList();
        return items;
    }
    public List<CategoryItem> CategoryItemList(int categoryId)
    {
        var items = _context.CategoryItems
            .Where(x => x.CategoryId == categoryId)
            .OrderBy(x => x.CategoryItemId)
            .ToList();
        return items;
    }
    // public List<ModifierItem> ModifierItemListByModifierGroupId(int ModifierGroupId)
    // {
    //     var items = _context.ModifierItems
    //         .Where(x => x.ModifierGroupId == ModifierGroupId)
    //         .OrderBy(x => x.ModifierGroupId)
    //         .ToList();
    //     return items;
    // }
    public CategoryItem GetItemById(int itemId)
    {
        return _context.CategoryItems
        .Include(x => x.CategoryModifierMappings)
        .ThenInclude(x => x.Modifier)
        .FirstOrDefault(x => x.CategoryItemId == itemId)!;
    }

    // public List<ModifierItem> GetModifierItemsList(int ModifierGroupId){
    //     return _context.ModifierItems
    //         .Where(x => x. == ModifierGroupId)
    //         .OrderBy(x => x.ModifierItemId)
    //         .ToList();
    // }
    public ModifierItem GetModifierItemById(int itemId)
    {
        return _context.ModifierItems.FirstOrDefault(x => x.ModifierItemId == itemId)!;
    }
    public List<ModifierGroup> GetAllModifierById()
    {
        return _context.ModifierGroups.ToList();
    }
    public MappingItemModifier GetMappingModifierItemById(int itemId)
    {
        return _context.MappingItemModifiers.FirstOrDefault(x => x.ModifierId == itemId)!;
    }
    // Delete category item from the database
    public void DeleteCategoryItem(int itemId)
    {
        var categoryItem = _context.CategoryItems.FirstOrDefault(x => x.CategoryItemId == itemId);
        if (categoryItem != null)
        {
            _context.CategoryItems.Remove(categoryItem);
            _context.SaveChanges();
        }

    }
    public void AddCategory(MenuViewModel model)
    {
        var category = new Category
        {
            CategoryName = model.CategoryName,
            Description = model.Description
        };
        _context.Categories.Add(category);
        _context.SaveChanges();
    }
    public CategoryItem AddCategoryItem(CategoryListViewModel model)
    {

        var categoryItem = new CategoryItem
        {
            CategoryId = model.CategoryId,
            ItemName = model.ItemName,
            Description = model.Description,
            Quantity = model.Quantity,
            Price = model.Price,
            DefaultTax = model.DefaultTax,
            UnitId = model.UnitId,
            IsAvailable = model.IsAvailable,
            ItemType = model.ItemType,
            ShortCode = model.ShortCode,
        };
        _context.CategoryItems.Add(categoryItem);
        _context.SaveChanges();

        return categoryItem;



    }
    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(p => p.CategoryId == id);
    }
    public void UpdateCategory(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }
    public void DeleteCategory(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
    public void DeleteMultipleCategoryItem(List<int> dataId)
    {
        var itemToDelete = _context.CategoryItems.Where(item => dataId.Contains(item.CategoryItemId)).ToList();

        if (itemToDelete != null)
        {
            _context.CategoryItems.RemoveRange(itemToDelete);
            _context.SaveChanges();
        }
    }


    public CategoryItem GetCategoryItemById(int id)
    {
        return _context.CategoryItems.FirstOrDefault(ci => ci.CategoryItemId == id);
    }

    public void UpdateCategoryItem(CategoryItem categoryItem)
    {
        _context.CategoryItems.Update(categoryItem);
        _context.SaveChanges();
    }




    #endregion
    #region Modifier

    public List<ModifierGroup> ModifierList()
    {
        var item = _context.ModifierGroups.OrderBy(c => c.ModifierName).ToList();
        return item;
    }

    public List<ModifierItem> ModifierItemsList()
    {
        var item = _context.ModifierItems
                    .Include(x => x.Unit)
                    .OrderBy(x => x.ModifierItemId).ToList();
        return item;
    }

    public List<Unit> UnitList()
    {
        return _context.Units.ToList();
    }


    public async Task<List<ModifierItem>> ModifierItemList(int ModifierGroupId)
    {
        try
        {
            var modifiers = _context.MappingItemModifiers.Where(c => c.ModifierGroupId == ModifierGroupId).Select(c => c.ModifierId).ToList();
            var result = await _context.ModifierItems.Where(c => modifiers.Contains(c.ModifierItemId)).Include(c => c.Unit).ToListAsync();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }





    public void DeleteModifierItem(int itemId)
    {
        var modifierItem = _context.ModifierItems.FirstOrDefault(x => x.ModifierItemId == itemId);
        if (modifierItem != null)
        {
            _context.ModifierItems.Remove(modifierItem);
            _context.SaveChanges();
        }
    }


    // public void AddModifier(MenuViewModel model)
    // {
    //     var ModifierGroup = new ModifierGroup
    //     {
    //         ModifierName = model.ModifierName,
    //         Description = model.Description
    //     };
    //     _context.ModifierGroups.Add(ModifierGroup);
    //     _context.SaveChanges();
    // }
    public ModifierGroup AddModifier(ModifierGroup modifier)
    {
        try
        {
            _context.ModifierGroups.Add(modifier);
            _context.SaveChanges();
            return modifier;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public MappingItemModifier AddModifierMapping(MappingItemModifier modifier)
    {
        try
        {
            _context.MappingItemModifiers.Add(modifier);
            _context.SaveChanges();
            return modifier;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    public CategoryModifierMapping AddCategoryModifierMapping(CategoryModifierMapping categoryModifierMapping)
    {
        try
        {
            _context.CategoryModifierMappings.Add(categoryModifierMapping);
            _context.SaveChanges();
            return categoryModifierMapping;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            throw;
        }


    }




    public ModifierItem AddModifierItem(ModifierListViewModel model)
    {
        // var modifierMapping = new MappingItemModifier{
        //     ModifierGroupId = model.ModifierGroupId,
        //     ModifierId = model.ModifierItemId

        // };

        var modifierItem = new ModifierItem
        {

            ModifierItemName = model.ModifierItemName,
            Description = model.Description,
            Quantity = model.Quantity,
            Rate = model.Rate,
            UnitId = model.UnitId,
        };
        _context.ModifierItems.Add(modifierItem);
        _context.SaveChanges();

        var ModifierItemList = _context.ModifierItems.FirstOrDefault(c => c.ModifierItemName == model.ModifierItemName);

        return ModifierItemList;
    }



    public ModifierGroup GetModifierGroupById(int id)
    {
        var name = _context.ModifierGroups.FirstOrDefault(p => p.ModifierGroupId == id);
        return name;
    }
    public Unit unitNameById(int id)
    {
        return _context.Units.FirstOrDefault(p => p.UnitId == id);

    }


    public void UpdateModifier(ModifierGroup modifier)
    {
        _context.ModifierGroups.Update(modifier);
        _context.SaveChanges();
    }


    public void DeleteModifier(int id)
    {
        var modifier = _context.ModifierGroups.FirstOrDefault(p => p.ModifierGroupId == id);

        if (modifier != null)
        {
            _context.ModifierGroups.Remove(modifier);
            _context.SaveChanges();
        }
    }

    public void UpdateModifierItem(ModifierItem modifierItem)
    {
        _context.ModifierItems.Update(modifierItem);
        _context.SaveChanges();
    }

    public void DeleteMultipleModifierItem(List<int> dataId)
    {
        // var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
        var itemToDelete = _context.ModifierItems.Where(item => dataId.Contains(item.ModifierItemId)).ToList();

        if (itemToDelete != null)
        {
            _context.ModifierItems.RemoveRange(itemToDelete);
            _context.SaveChanges();
        }
    }

    #endregion
}

