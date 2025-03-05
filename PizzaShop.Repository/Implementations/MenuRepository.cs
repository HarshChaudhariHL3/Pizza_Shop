using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class MenuRepository(PizzaShopDbContext _context) : IMenuRepository
{
    public List<Category> CategoryList() {

        var items = _context.Categories.ToList(); 
         
        return items;
        
    }
}
