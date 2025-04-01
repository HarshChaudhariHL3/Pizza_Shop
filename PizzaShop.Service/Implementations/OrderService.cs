using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class OrderService(IOrderRepository _orderRepository) : IOrderService
{
    public async Task<PaginationViewModel<OrderViewModel>> GetOrderDetail(int page, int pageSize, string search = "", string orderStatus = "", string orderTime = "", DateTime? fromDate = null,
    DateTime? toDate = null)
    {

        List<Order> orders = _orderRepository.GetAllOrderList();
        List<OrderViewModel> orderListViews = new List<OrderViewModel>();

        foreach (Order item in orders)
        {
            orderListViews.Add(new OrderViewModel
            {
                OrderId = item.OrderId,
                CustomerName = item.Customer.CustomerName,
                PaymentMethod = item.Payment.PaymentMethod,
                OrderStatus = item.Status,
                OrderDate = item.CreatedAt,
                TotalAmount = item.TotalAmount,
                Rating = item.Feedbacks.FirstOrDefault()?.Rating,

            });
        }
        if (!string.IsNullOrEmpty(search))
        {
            orderListViews = orderListViews.Where(u => u.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(orderStatus))
        {
            orderListViews = orderListViews
                .Where(o => o.OrderStatus.Equals(orderStatus, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (!string.IsNullOrEmpty(orderTime) && orderTime != "all")
        {
            DateTime now = DateTime.Now;

            if (orderTime == "7")
            {
                var last7Days = now.AddDays(-7);
                orderListViews = orderListViews.Where(o => o.OrderDate >= last7Days).ToList();
            }
            else if (orderTime == "30")
            {
                var last30Days = now.AddDays(-30);
                orderListViews = orderListViews.Where(o => o.OrderDate >= last30Days).ToList();
            }
            else if (orderTime == "month")
            {
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                orderListViews = orderListViews.Where(o => o.OrderDate >= startOfMonth).ToList();
            }
        }
        if (fromDate.HasValue && toDate.HasValue)
        {

            orderListViews = orderListViews
                .Where(o => o.OrderDate >= fromDate.Value.Date && o.OrderDate <= toDate.Value.Date)
                .ToList();
        }
        else if (fromDate.HasValue)
        {
            orderListViews = orderListViews
                .Where(o => o.OrderDate >= fromDate.Value.Date)
                .ToList();
        }
        else if (toDate.HasValue)
        {
            orderListViews = orderListViews
                .Where(o => o.OrderDate <= toDate.Value.Date)
                .ToList();
        }

        var tableCount = orderListViews.Count;
        orderListViews = orderListViews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<OrderViewModel>
        {
            Items = orderListViews,
            TotalItems = tableCount,
            CurrentPage = page,
            PageSize = pageSize,
        };
    }


    public IQueryable<Order> GetAllOrderDetailToExport()
    {
        var element = _orderRepository.GetAllOrderList().AsQueryable();
        return element;
    }


    public OrderSummaryViewModel GetOrderDetails(long id)
    {
        return _orderRepository.GetOrderDetails(id);
    }

    public ExportOrderResultViewModel GetExportOrders(string search, string time, DateTime fromDate, DateTime toDate, string status = "")
    {
        var query = _orderRepository.GetAllOrderExport();


        if (!string.IsNullOrEmpty(time) && time != "all")
        {
            DateTime now = DateTime.Now;

            if (time == "7")
            {
                var last7Days = now.AddDays(-7);
                query = query.Where(o => o.CreatedAt >= last7Days);
            }
            else if (time == "30")
            {
                var last30Days = now.AddDays(-30);
                query = query.Where(o => o.CreatedAt >= last30Days);
            }
            else if (time == "month")
            {
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                query = query.Where(o => o.CreatedAt >= startOfMonth);
            }
        }

        // Status filter
        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(o => o.Status == status);
        }

        // Search filter
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(o =>
                o.Customer.CustomerName.Contains(search) ||
                o.Payment.PaymentMethod.Contains(search));
            // o.OrderId.Contains(search) ||
        }


        if (fromDate != default(DateTime))
        {
            query = query.Where(x => x.CreatedAt > fromDate);
        }
        if (toDate != default(DateTime))
        {
            query = query.Where(x => x.CreatedAt < toDate);
        }

        var result = query.Select(o => new OrderViewModel
        {
            OrderId = o.OrderId,
            OrderDate = o.CreatedAt,
            CustomerName = o.Customer.CustomerName,
            OrderStatus = o.Status,
            PaymentMethod = o.Payment.PaymentMethod,
            Rating = o.Feedbacks != null && o.Feedbacks.Any() ? o.Feedbacks.FirstOrDefault().Rating : null,
            TotalAmount = o.TotalAmount,
        }).ToList();

        return new ExportOrderResultViewModel
        {
            status = status,
            search = search,
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            record = result.Count.ToString(),
            orderData = result
        };
    }
}

