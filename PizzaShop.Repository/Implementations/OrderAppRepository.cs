using PizzaShop.Entity.Data;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderAppRepository(PizzaShopDbContext _context) : IOrderAppRepository
{
}
