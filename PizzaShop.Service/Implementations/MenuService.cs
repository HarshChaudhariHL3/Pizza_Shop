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

    public List<CategoryListViewModel> GetCategoryItemsByCategoryId(int categoryId)
{
    // Fetch category items from the repository by categoryId
    var categoryItems = _menuRepository.CategoryItemList(categoryId);

    // Map category items to the CategoryListViewModel
    var categoryItemViewModels = categoryItems.Select(item => new CategoryListViewModel
    {
        CategoryItemId = item.CategoryItemId,
        CategoryId = item.CategoryId,
        ItemName = item.ItemName,
        Description = item.Description,
        Quantity = item.Quantity,
        Price = item.Price,
        IsAvailable = item.IsAvailable,
        ShortCode = item.ShortCode,
        ImageUrl = item.ImageUrl
    }).ToList();

    return categoryItemViewModels;
}

    

    public void AddCategory(MenuViewModel model){
        _menuRepository.AddCategory(model);
    }

    public MenuViewModel GetCategoryById(int id){
        var category = _menuRepository.GetCategoryById(id); 
        
        return new MenuViewModel{
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description
        };
    }

    public void EditCategory(MenuViewModel model){
        var category = _menuRepository.GetCategoryById(model.CategoryId);
        if(category != null){
            category.CategoryName = model.CategoryName;
            category.Description = model.Description;
            _menuRepository.UpdateCategory(category);
        }
    }

    public void RemoveCategory(int id){
        _menuRepository.DeleteCategory(id);
    }

}
