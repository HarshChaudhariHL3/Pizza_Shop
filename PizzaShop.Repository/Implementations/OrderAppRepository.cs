using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderAppRepository(PizzaShopDbContext _context) : IOrderAppRepository
{
    public List<Category> GetCategoryList(){
        var CategoryList = _context.Categories.ToList();
        return CategoryList;
    }
}
