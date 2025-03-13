using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class MenuRepository(PizzaShopDbContext _context) : IMenuRepository
{
    public List<Category> CategoryList()
    {
        var items = _context.Categories.OrderBy(c => c.CategoryName).ToList();
        return items;
    }

    public List<Unit> UnitList(){
        return _context.Units.ToList();
    }

    public List<CategoryItem> CategoryItemList(int categoryId)
    {
        var items = _context.CategoryItems
            .Where(x => x.CategoryId == categoryId)
            .OrderBy(x => x.CategoryItemId)
            .ToList();
        return items;
    }

    public CategoryItem GetItemById(int itemId)
    {
        return _context.CategoryItems.FirstOrDefault(x => x.CategoryItemId == itemId)!;
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

    public void AddCategoryItem(CategoryListViewModel model){

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
    public CategoryItem GetCategoryItemById(int id)
    {
        return _context.CategoryItems.FirstOrDefault(ci => ci.CategoryItemId == id);
    }

    public void UpdateCategoryItem(CategoryItem categoryItem)
    {
        _context.CategoryItems.Update(categoryItem);
        _context.SaveChanges();
    }
}

