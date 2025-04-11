using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IOrderAppRepository
{
    List<Category> GetCategoryList();
    List<Section> GetTableList();
    List<Section> GetSections();
    void AddWaitingList(WaitingListViewModel model);
    // List<Order> GetKOTCardDetails();
}
