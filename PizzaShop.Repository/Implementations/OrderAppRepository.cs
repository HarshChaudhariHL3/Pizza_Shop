using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderAppRepository(PizzaShopDbContext _context) : IOrderAppRepository
{
    public List<ModifierGroup> GetModifierGroupList(){
        var modifierGroupList = _context.ModifierGroups.ToList();
        return modifierGroupList;
    }
}
