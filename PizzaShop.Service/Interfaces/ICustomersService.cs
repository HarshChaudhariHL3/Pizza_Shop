using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface ICustomersService
{
    Task<PaginationViewModel<CustomerViewModel>> GetCustomerDetail(int page, int pageSize, string search = "", string customerTime = "");
    CustomerHistoryViewModel GetCustomerHistory(int id);

    ExportOrderResultViewModel GetExportCustomer(string search, string time);
}
