using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderRepository(PizzaShopDbContext _context) : IOrderRepository
{
    public List<Order> GetAllOrderList()
    {
        try{

        var items = _context.Orders
            // .Where(x.IsDeleted == false)
            .Include(x => x.Payment)
            .Include(x => x.Customer)
            .Include(x => x.Feedbacks)
            .ToList();
            return items;
        } catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
                throw;
                // return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
                throw;
                // return null;
            }
    }

    // public Feedback feedbacks(int OrderId){
    //     return _context.Feedbacks.FirstOrDefault(x => x.OrderId == OrderId);
    // }
}
