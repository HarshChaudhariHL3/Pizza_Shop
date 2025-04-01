using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class CustomersRepository(PizzaShopDbContext _context) : ICustomersRepository
{
    public List<Customer> GetAllCustomerList()
    {
        return _context.Customers.ToList();
    }

    public IQueryable<Customer> GetAllCustomerExport()
    {
        try

        {
            var query = _context.Customers.AsQueryable();
            return query;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return null;
        }
    }

    public List<Order> GetTotalOrderCount(string Email)
    {   
        var query = _context.Orders.Where(o => o.Customer.Email == Email).ToList();
        return query;
    }
        
}
