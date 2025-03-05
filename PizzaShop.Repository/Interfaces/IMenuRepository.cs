using PizzaShop.Entity.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IMenuRepository
{
    List<Category> CategoryList();
}
