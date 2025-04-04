using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    #region Category
    public List<Category> GetCategories()
    {
        return _menuRepository.CategoryList();
    }

    public async Task<PaginationViewModel<CategoryListViewModel>> GetCategoryItems(int categoryId, int page, int pageSize, string search = "")
    {
        // if(page == 0){
        //     page = 1;
        // }

        // if(pageSize == 0){
        //     pageSize = 5;
        // }

        List<CategoryItem> categoryItems = _menuRepository.CategoryItemList(categoryId);
        List<CategoryListViewModel> categoryListViews = new List<CategoryListViewModel>();

        foreach (CategoryItem item in categoryItems)
        {
            categoryListViews.Add(new CategoryListViewModel
            {
                CategoryItemId = item.CategoryItemId,
                CategoryId = item.CategoryId,
                ItemName = item.ItemName,
                Description = item.Description,
                Quantity = item.Quantity,
                Price = item.Price,
                ItemType = item.ItemType,
                IsAvailable = item.IsAvailable ?? false,
                ShortCode = item.ShortCode,
                ImageUrl = item.ImageUrl,
                UnitId = item.UnitId
            });
        }
        if (!string.IsNullOrEmpty(search))
        {
            categoryListViews = categoryListViews.Where(u => u.ItemName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        int catItemCount = categoryListViews.Count;
        categoryListViews = categoryListViews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<CategoryListViewModel>
        {
            Items = categoryListViews,
            TotalItems = catItemCount,
            CurrentPage = page,
            PageSize = pageSize,
        };

    }

    public CategoryListViewModel GetItemById(int itemId)
    {
        var item = _menuRepository.GetItemById(itemId);
        if (item == null)
            return null;
        // var ModifierItemsList = _menuRepository.GetModifierItemsList(item.CategoryModifierMappings.First().);

        return new CategoryListViewModel
        {
            CategoryItemId = item.CategoryItemId,
            CategoryId = item.CategoryId,
            ItemName = item.ItemName,
            Description = item.Description,
            Quantity = item.Quantity,
            DefaultTax = item.DefaultTax,
            UnitId = item.UnitId,
            Price = item.Price,
            ItemType = item.ItemType,
            IsAvailable = item.IsAvailable ?? false,
            // TaxPercentage = item.TaxPercentage,
            ShortCode = item.ShortCode,
            ImageUrl = item.ImageUrl,
            SelectedModifiers = item.CategoryModifierMappings.Select(x => new CategoryModifierMappingsViewModel
            {
                ModifierGroupId = x.Modifier.ModifierGroupId,
                ModifierGroupName = x.Modifier.ModifierName,
                CategoryItemId = x.CategoryItemId,
                ModifierId = x.ModifierId?? 0,
                MaxValue = x.MaxValue,
                MinValue = x.MinValue,
                // ModifierItems = x.Modifier.MappingItemModifiers.ToList()
            }).ToList(),

        };
    }

    public void AddCategory(MenuViewModel model)
    {
        _menuRepository.AddCategory(model);
    }

    public MenuViewModel GetCategoryById(int id)
    {
        var category = _menuRepository.GetCategoryById(id);

        return new MenuViewModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
    }

    public void EditCategory(MenuViewModel model)
    {
        var category = _menuRepository.GetCategoryById(model.CategoryId);
        if (category != null)
        {
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            _menuRepository.UpdateCategory(category);
        }
    }
    public bool DeleteCategoryItem(int itemId)
    {
        if (itemId != null)
        {
            _menuRepository.DeleteCategoryItem(itemId);
            return true;
        }
        return false;
    }
    public bool DeleteMultipleCategoryItem(List<int> dataId)
    {


        _menuRepository.DeleteMultipleCategoryItem(dataId);
        return true;

    }

    public void EditCategoryItem(CategoryListViewModel model)
    {
        var categoryItem = _menuRepository.GetCategoryItemById(model.CategoryItemId);

        if (categoryItem != null)
        {
            categoryItem.CategoryItemId = model.CategoryItemId;
            categoryItem.ItemName = model.ItemName;
            categoryItem.Description = model.Description;
            categoryItem.Quantity = model.Quantity;
            categoryItem.Price = model.Price;
            categoryItem.DefaultTax = model.DefaultTax;
            categoryItem.UnitId = model.UnitId;
            categoryItem.IsAvailable = model.IsAvailable;
            categoryItem.ItemType = model.ItemType;
            categoryItem.ShortCode = model.ShortCode;

            _menuRepository.UpdateCategoryItem(categoryItem);
        }
    }

    public void AddCategoryItem(CategoryListViewModel model)
    {
        var categoryItemList = _menuRepository.AddCategoryItem(model);


        for (int i = 0; i < model.SelectedModifiers.Count; i++)
        {

            var categoryModifierMapping = new CategoryModifierMapping
            {
                CategoryItemId = categoryItemList.CategoryItemId,
                ModifierId = model.SelectedModifiers[i].ModifierId,
                MaxValue = model.SelectedModifiers[i].MaxValue,
                MinValue = model.SelectedModifiers[i].MinValue
            };
            _menuRepository.AddCategoryModifierMapping(categoryModifierMapping);
        }

        // _menuRepository.AddModifierMapping(modifierMapping);
    }



    #endregion
    #region Modifier



    public List<ModifierGroup> GetModifierGroups()
    {
        return _menuRepository.ModifierList();
    }
    public List<ModifierItem> ModifierItemsList()
    {
        return _menuRepository.ModifierItemsList();
    }
    public List<ExistingModifierViewModel> GetModifierItems()
    {
        var items = _menuRepository.ModifierItemsList();

        var model = new List<ExistingModifierViewModel>();
        foreach (var i in items)
        {
            model.Add(new ExistingModifierViewModel
            {
                ModifierItemId = i.ModifierItemId,
                ModifierItemName = i.ModifierItemName,
                Quantity = i.Quantity,
                Rate = i.Rate,
                UnitId = i.UnitId,
                UnitName = i.Unit?.UnitName,
                Description = i.Description
            });
        }
        return model;
    }
        public async Task<PaginationViewModel<ExistingModifierViewModel>> GetModifierItems(int page, int pageSize, string search = "")
    {

        var items = _menuRepository.ModifierItemsList();
        List<ExistingModifierViewModel> modifierListViews = new List<ExistingModifierViewModel>();

        foreach (ModifierItem item in items)
        {
            modifierListViews.Add(new ExistingModifierViewModel
            {
                ModifierItemId = item.ModifierItemId,
                ModifierItemName = item.ModifierItemName,
                Quantity = item.Quantity,
                UnitName = item.Unit?.UnitName,
                Rate = item.Rate,
            });
        }
        if (!string.IsNullOrEmpty(search))
        {
            modifierListViews = modifierListViews.Where(u => u.ModifierItemName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        int catItemCount = modifierListViews.Count;
        modifierListViews = modifierListViews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<ExistingModifierViewModel>
        {
            Items = modifierListViews,
            TotalItems = catItemCount,
            CurrentPage = page,
            PageSize = pageSize,
        };
    }
    public List<Unit> UnitList()
    {
        return _menuRepository.UnitList();
    }
    public void RemoveCategory(int id)
    {
        _menuRepository.DeleteCategory(id);
    }

    public async Task<PaginationViewModel<ModifierListViewModel>> GetModifierItems(int ModifierGroupId, int page, int pageSize, string search = "")
    {

        var modifierItem = await _menuRepository.ModifierItemList(ModifierGroupId);
        List<ModifierListViewModel> modifierListViews = new List<ModifierListViewModel>();

        foreach (ModifierItem item in modifierItem)
        {
            modifierListViews.Add(new ModifierListViewModel
            {
                ModifierGroupId = ModifierGroupId,
                ModifierItemId = item.ModifierItemId,
                ModifierItemName = item.ModifierItemName,
                Quantity = item.Quantity,
                UnitName = item.Unit?.UnitName,
                Rate = item.Rate,
            });
        }
        if (!string.IsNullOrEmpty(search))
        {
            modifierListViews = modifierListViews.Where(u => u.ModifierItemName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        int catItemCount = modifierListViews.Count;
        modifierListViews = modifierListViews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<ModifierListViewModel>
        {
            Items = modifierListViews,
            TotalItems = catItemCount,
            CurrentPage = page,
            PageSize = pageSize,
        };
    }

    // Fetch item details by ItemId
    public async Task<List<ModifierListViewModel>> GetModifierItemById(int itemId)
    {
        var item = await _menuRepository.ModifierItemList(itemId);
        var modifierGroupName = _menuRepository.GetModifierGroupById(itemId);

        var modifier = item.Select(x => new ModifierListViewModel
        {
            ModifierGroupName = modifierGroupName.ModifierName,
            ModifierItemName = x.ModifierItemName,
            ModifierItemId = x.ModifierItemId,
            Rate = x.Rate,
            Quantity = x.Quantity,
            UnitId = x.UnitId,
            UnitName = x.Unit.UnitName,
            Description = x.Description
        }).ToList();

        return modifier;
    }
    public List<ModifierGroup> GetAllModifierById()
    {
        return _menuRepository.GetAllModifierById().ToList();

    }


    public async Task AddModifier(MenuViewModel model)
    {
        var modifierGroup = new ModifierGroup
        {
            ModifierName = model.ModifierName,
            Description = model.DescriptionModifier
        };

        _menuRepository.AddModifier(modifierGroup);

        foreach (var itemId in model.SelectedModifierIds)
        {
            var itemModifierGroup = new MappingItemModifier
            {
                ModifierId = itemId,
                ModifierGroupId = modifierGroup.ModifierGroupId
            };
            _menuRepository.AddModifierMapping(itemModifierGroup);

        }
    }


    public void EditModifier(MenuViewModel model)
    {
        var modifier = _menuRepository.GetModifierGroupById(model.ModifierGroupId);
        if (modifier != null)
        {
            modifier.ModifierName = model.ModifierName;
            modifier.Description = model.DescriptionModifier;
            _menuRepository.UpdateModifier(modifier);
        }
    }


    public void RemoveModifier(int id)
    {
        _menuRepository.DeleteModifier(id);
    }


    public bool DeleteModifierItem(int ModifierItemId)
    {
        var Item = _menuRepository.GetModifierItemById(ModifierItemId);
        if (Item != null)
        {
            _menuRepository.DeleteModifierItem(ModifierItemId);
            return true;
        }
        return false;
    }


    public void EditModifierItem(ModifierListViewModel model)
    {

        var item = _menuRepository.GetModifierItemById(model.ModifierItemId);

        if (item != null)
        {
            item.ModifierItemName = model.ModifierItemName;
            item.Description = model.Description;
            item.Quantity = model.Quantity;
            item.UnitId = model.UnitId;
            item.Rate = model.Rate;

            _menuRepository.UpdateModifierItem(item);
        }
    }

    public void AddModifierItem(ModifierListViewModel model)
    {


        var ModifierItemList = _menuRepository.AddModifierItem(model);

        var modifierMapping = new MappingItemModifier
        {
            ModifierGroupId = model.ModifierGroupId,
            ModifierId = ModifierItemList.ModifierItemId

        };
        _menuRepository.AddModifierMapping(modifierMapping);

    }


    public bool DeleteMultipleModifierItem(List<int> dataId)
    {
        _menuRepository.DeleteMultipleModifierItem(dataId);
        return true;
    }


    #endregion
}


