using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class PaginationRepository(PizzaShopDbContext _context) : IPaginationRepository
{
    public List<User> pagination_user_list(int page, int page_size, string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return _context.Users.Where(u => u.Isdeleted != true)
            .OrderBy(u => u.UserId)
            .Skip((page - 1) * page_size)
            .Take(page_size)
            .ToList();
        }
        return _context.Users.Where(u => u.FirstName.ToLower().Contains(search.ToLower()) || u.LastName.ToLower().Contains(search.ToLower()) || u.Email.ToLower().Contains(search.ToLower()))
            .Where(u => u.Isdeleted == false)
            .OrderBy(u => u.UserId)
            .Skip((page - 1) * page_size)
            .Take(page_size)
            .ToList();
    }

    public int get_usercount(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return _context.Users.Where(u => u.Isdeleted == false).Count();
        }
        else
        {
            var count = _context.Users.Where(u => u.FirstName.ToLower().Contains(search.ToLower()) || u.LastName.ToLower().Contains(search.ToLower()) || u.Email.ToLower().Contains(search.ToLower()))
            .Where(u => u.Isdeleted == false)
            .Count();
            return count;
        }
    }
}
