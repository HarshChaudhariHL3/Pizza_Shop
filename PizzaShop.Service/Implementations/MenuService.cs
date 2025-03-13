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

    public List<Category> GetCategories()
    {
        return _menuRepository.CategoryList();
    }
    public  List<Unit> UnitList(){
        return _menuRepository.UnitList();
    }

    public List<CategoryListViewModel> GetCategoryItemsByCategoryId(int categoryId)
    {
        var categoryItems = _menuRepository.CategoryItemList(categoryId);

        var categoryItemViewModels = categoryItems.Select(item => new CategoryListViewModel
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
        }).ToList();

        return categoryItemViewModels;
    }
    
    // Fetch item details by ItemId
    public CategoryListViewModel GetItemById(int itemId)
    {
        var item = _menuRepository.GetItemById(itemId);
        if (item == null)
            return null;

        // Map data from entity to ViewModel
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
            ImageUrl = item.ImageUrl
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

    public void RemoveCategory(int id)
    {
        _menuRepository.DeleteCategory(id);
    }

     public bool DeleteCategoryItem(int itemId)
    {
        var categoryItem = _menuRepository.GetCategoryItemById(itemId);
        if (categoryItem != null)
        {
            _menuRepository.DeleteCategoryItem(itemId);
            return true;
        }
        return false;
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
            _menuRepository.AddCategoryItem(model);
    }
}


