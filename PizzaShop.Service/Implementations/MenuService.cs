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

    public List<Category> GetCategories(){
        return _menuRepository.CategoryList();
    }

    public void AddCategory(MenuViewModel model){
        _menuRepository.AddCategory(model);
    }

    public void DeleteCategory(int id){
        _menuRepository.DeleteCategory(id);
    }

    public MenuViewModel EditCategory(MenuViewModel model){      
        
        var id = model.CategoryId;

        var category = _menuRepository.getCategoryById(id);

        if(category != null)
        {
        category.CategoryName = model.CategoryName;
        category.Description = model.Description;
        _menuRepository.EditCategory(category);
        }

        return model;
    }
}
