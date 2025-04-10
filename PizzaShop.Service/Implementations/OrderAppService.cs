using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class OrderAppService(IOrderAppRepository _orderAppRepository) : IOrderAppService
{
    public KotViewModel GetCategoryList()
    {
        var CategoryList = _orderAppRepository.GetCategoryList();

        var CategoryViewModelList = new KotViewModel
        {
            CategoryList = new List<KotCategory>()
        };
        foreach (var modifierGroup in CategoryList)
        {
            var CategoryViewModel = new KotCategory
            {
                CategoryId = modifierGroup.CategoryId,
                CategoryName = modifierGroup.CategoryName
            };
            CategoryViewModelList.CategoryList.Add(CategoryViewModel);
        }

        return  CategoryViewModelList;
    }


    // public KotViewModel GetKOTCardDetails(int categoryId , string progress ){




    // }
}
