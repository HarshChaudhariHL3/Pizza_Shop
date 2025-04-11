using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IOrderAppService
{
    KotViewModel GetCategoryList();
    TableOrderAppViewModel GetTableList();
    List<Section> GetSections();
    void AddWaitingList(WaitingListViewModel model);
}
