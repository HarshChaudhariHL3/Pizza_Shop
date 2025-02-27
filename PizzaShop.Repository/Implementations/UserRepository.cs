using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class UserRepository (PizzaShopContext _context) : IUserRepository
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

    public List<Country> GetCountry()
    {
        return _context.Countries.ToList();
    }

    public  List<State> GetState(int country_id)
    {

        return _context.States.Where(s => s.CountryId == country_id).ToList();
    }

    public  List<City> GetCity(int state_id)
    {

        return _context.Cities.Where(c => c.StateId == state_id).ToList();
    }
}
