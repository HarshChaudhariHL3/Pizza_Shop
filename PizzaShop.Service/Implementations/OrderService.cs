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
        // if (fromDate.HasValue)
        // {
        //     orderListViews = orderListViews.Where(o => o.OrderDate >= fromDate.Value).ToList();
        // }

        // if (toDate.HasValue)
        // {
        //     orderListViews = orderListViews.Where(o => o.OrderDate <= toDate.Value).ToList();
        // }
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
}
