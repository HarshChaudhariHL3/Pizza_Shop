using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class OrderAppRepository(PizzaShopDbContext _context) : IOrderAppRepository
{
    public List<Category> GetCategoryList(){
        var CategoryList = _context.Categories.ToList();
        return CategoryList;
    }

    // public List<Order> GetKOTCardDetails(){
    //     var orderDetails = _context.Orders
    //         .Include(x => x.TableOrderMappings)
    //         .Include(x => x.OrderItems)
    //         .ThenInclude(x => x.OrderItemModifiers)
    //         .ToList();
    //     return orderDetails;
    // }

    public List<Section> GetTableList()
    {
        var TableList = _context.Sections
                        .Include(x => x.TableDetails)
                        .Where(x => x.IsDeleted == false)
                        .ToList();
        return TableList;
    }

    public List<Section> GetSections()
    {
        return _context.Sections
        .Where(x => x.IsDeleted == false)
        .OrderBy(x => x.SectionId)
        .ToList();
    }

    public void AddWaitingList(WaitingListViewModel model)
    {
        var WaitingList = new WaitingList
        {
            Email = model.Email,
            Phone  = model.PhoneNumber,
            TotalPerson = model.NoOfPerson,
            SectionId = model.SectionId,
           UserName = model.UserName
        };
        _context.WaitingLists.Add(WaitingList);
        _context.SaveChanges();
    }
    
}
