using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IOrderService
{
    Task<PaginationViewModel<OrderViewModel>> GetOrderDetail(int page, int pageSize, string search = "", string orderStatus = "", string orderTime = "",DateTime? fromDate = null,
    DateTime? toDate = null);

    IQueryable<Order> GetAllOrderDetailToExport ();
    OrderSummaryViewModel GetOrderDetails(long id);

    ExportOrderResultViewModel GetExportOrders(string search, string time, DateTime fromDate, DateTime toDate, string status = ""); 
}
