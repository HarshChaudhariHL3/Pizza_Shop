using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderRepository(PizzaShopDbContext _context) : IOrderRepository
{
    public List<Order> GetAllOrderList()
    {
        try
        {

            var items = _context.Orders
                // .Where(x.IsDeleted == false)
                .Include(x => x.Payment)
                .Include(x => x.Customer)
                .Include(x => x.Feedbacks)
                .ToList();
            return items;
        }
        catch (DbUpdateException ex)
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

    public OrderSummaryViewModel GetOrderDetails(long id)
    {
        try
        {
            // var taxQuery =  _context.OrderTaxMappings.Where(x=>x.OrderId == id).Select(tax => new TaxesAndFeesViewModel{
            //             TaxName = tax.Tax.TaxName,
            //             TaxValue = tax.Tax.TaxValue,
            //         }).ToList();

            var tableQuery = _context.TableOrderMappings.Where(x => x.OrderId == id).Select(table => new TableAndSectionViewModel
            {
                TableName = table.Table.TableName,
                // Name = table.Table.Section.Name,
                // Capacity = table.Capacity
            }).FirstOrDefault();

            var Query = _context.Orders.Where(x => x.OrderId == id).Select(order => new OrderSummaryViewModel
            {
                // tax = taxQuery,
                table = tableQuery!,
                CustomerName = order.Customer.CustomerName,
                Phone = order.Customer.Phone,
                Email = order.Customer.Email,
                OrderStatus = order.Status,
                CreatedAt = (DateTime)order.CreatedAt,
                ModifiedAt = (DateTime)order.ModifiedAt!,
                // items = order.OrderedItems.Where(x=>x.OrderId == id).Select(m =>new ItemsViewModel{
                //     ItemName = m.MenuItem.ItemName,
                //     Quantity = m.MenuItem.Quantity,

                //         modifier = m.OrderedItemModifierMappings.Where(u=>u.OrderItemId == m.Id).Select(n=>new ModifierViewModel(){
                //             ModifierName = n.Modifier.ModifierItem.Name
                //         }).ToList()
                // }).ToList(),
            }).FirstOrDefault();

            return Query!;
        }
        catch (Exception Ex)
        {
            Console.WriteLine(Ex.Message);
            return new OrderSummaryViewModel();
        }
    }

    public IQueryable<Order> GetAllOrderExport()
    {
        try

        {
            var query = _context.Orders.AsNoTracking().Include(e => e.Customer)
                        .Include(e => e.Feedbacks)
                        .Include(e => e.Payment)
                        .AsQueryable();
            return query;
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine($"DbUpdateException: {ex.Message}\n{ex.InnerException?.Message}");
            // throw;
            return null;
        }

    }


}
