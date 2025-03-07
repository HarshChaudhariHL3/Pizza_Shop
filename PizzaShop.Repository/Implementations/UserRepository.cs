using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class UserRepository (PizzaShopDbContext _context) : IUserRepository
{
    public  User GetAll(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
        {
            throw new Exception($"User with id {id} not found.");
        }
        return user;
    }

    public  User GetAllByEmail(string email)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            throw new Exception($"User with email {email} not found.");
        }
        return user;
    }

    public Role GetRole(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserId == id);

        if (user == null)
        {
            throw new Exception($"User with id {id} not found.");
        }
        var role = _context.Roles.FirstOrDefault(r => r.RoleId == user.UserRole);
        if (role == null)
        {
            throw new Exception($"Role with id {user.UserRole} not found.");
        }
        return role;
    }

    public List<Country> GetCountry()
    {
        return _context.Countries.ToList();
   }

    public List<Role> GetRoleList()
    {
        return _context.Roles.ToList();
    }

    public  List<State> GetState(int country_id)
    {
        return _context.States.Where(s => s.CountryId == country_id).ToList();
    }

    public  List<City> GetCity(int state_id)
    {

        return _context.Cities.Where(c => c.StateId == state_id).ToList();
    }

    public List<User> pagination_user_list(int page, int page_size, string search)
    {
        if(string.IsNullOrEmpty(search))
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
        if(string.IsNullOrEmpty(search))
        {
            return _context.Users.Where(u => u.Isdeleted == false).Count();
        }else{
            var count = _context.Users.Where(u => u.FirstName.ToLower().Contains(search.ToLower()) || u.LastName.ToLower().Contains(search.ToLower())|| u.Email.ToLower().Contains(search.ToLower()))
            .Where(u => u.Isdeleted == false)
            .Count();
            return count;
        }
    }

    public bool Update(User user)
    {

        _context.Users.Update(user);
        return _context.SaveChanges() > 0;
    }

    public bool Add(User user)
    {
        _context.Users.Add(user);
        return _context.SaveChanges() > 0;
    }

    public bool Delete(User user)
    {
        user.Isdeleted = true;
        // _context.Users.Remove(user);
        _context.Users.Update(user);
        return _context.SaveChanges() > 0;
    }

}
