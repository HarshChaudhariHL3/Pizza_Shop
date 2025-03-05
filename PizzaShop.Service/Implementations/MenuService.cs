using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class MenuService(IMenuRepository _menuRepository) : IMenuService
{
    public List<Category> GetCategories(){
        return _menuRepository.CategoryList();
    }
}
