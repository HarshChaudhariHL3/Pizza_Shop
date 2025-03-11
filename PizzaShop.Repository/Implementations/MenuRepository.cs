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

    public List<CategoryItem> CategoryItemList(int categoryId)
    {
        var items =  _context.CategoryItems
            .Where(x => x.CategoryId == categoryId)
            .ToList();
        return items;
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

}