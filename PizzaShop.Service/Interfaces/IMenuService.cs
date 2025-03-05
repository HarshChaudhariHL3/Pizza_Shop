using PizzaShop.Entity.Models;

namespace PizzaShop.Service.Interfaces;

public interface IMenuService
{
    List<Category> GetCategories();
}
