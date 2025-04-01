using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class CustomersService(ICustomersRepository _customersRepository) : ICustomersService
{
    public async Task<PaginationViewModel<CustomerViewModel>> GetCustomerDetail(int page, int pageSize, string search = "", string customerTime = "")
    {

        List<Customer> customers = _customersRepository.GetAllCustomerList();
        List<CustomerViewModel> customerListViews = new List<CustomerViewModel>();
        foreach (Customer item in customers)
        {
            var totalOrder = item.Email != null 
            ? _customersRepository.GetTotalOrderCount(item.Email).Count 
            : 0;

            customerListViews.Add(new CustomerViewModel
            {
            CustomerName = item.CustomerName,
            Email = item.Email,
            Phone = item.Phone,
            Date = item.CreatedAt,
            TotalOrder = totalOrder
            });
        }
        if (!string.IsNullOrEmpty(search))
        {
            customerListViews = customerListViews.Where(u => u.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }



        if (!string.IsNullOrEmpty(customerTime) && customerTime != "all")
        {
            DateTime now = DateTime.Now;

            if (customerTime == "7")
            {
                var last7Days = now.AddDays(-7);
                customerListViews = customerListViews.Where(o => o.Date >= last7Days).ToList();
            }
            else if (customerTime == "30")
            {
                var last30Days = now.AddDays(-30);
                customerListViews = customerListViews.Where(o => o.Date >= last30Days).ToList();
            }
            else if (customerTime == "month")
            {
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                customerListViews = customerListViews.Where(o => o.Date >= startOfMonth).ToList();
            }
        }


        var tableCount = customerListViews.Count;
        customerListViews = customerListViews.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return new PaginationViewModel<CustomerViewModel>
        {
            Items = customerListViews,
            TotalItems = tableCount,
            CurrentPage = page,
            PageSize = pageSize,
        };
    }


    public ExportOrderResultViewModel GetExportCustomer(string search, string time)
    {
        var query = _customersRepository.GetAllCustomerExport();
        var emailCounts = query
                .GroupBy(o => o.Email)
                .Select(group => new
                {
                    Email = group.Key,
                    Count = group.Count()
                }).ToDictionary(x => x.Email, x => x.Count);

        // int parsedTime = int.TryParse(time, out var result) ? result : 0;
        // var Date = DateTime.Now.AddDays(-parsedTime);

        // if (parsedTime >= 0)
        // {
        //     query = query.Where(o => o.CreatedAt >= Date);
        // }

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

        // Search filter
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(o =>
                o.CustomerName.Contains(search));

        }

        var customerData = query.Select(o => new CustomerViewModel
        {
            CustomerId = o.CustomerId,
            Date = o.CreatedAt,
            CustomerName = o.CustomerName,
            Email = o.Email,
            Phone = o.Phone,
            TotalOrder = emailCounts.ContainsKey(o.Email) ? emailCounts[o.Email] : 0
        }).ToList();

        return new ExportOrderResultViewModel
        {

            search = search,
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            record = customerData.Count.ToString(),
            CustomerData = customerData,
            
        };
    }
}
