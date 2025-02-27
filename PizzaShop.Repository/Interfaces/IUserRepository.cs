using PizzaShop.Entity.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IUserRepository
{
     User GetAll(int id);

    List<Country> GetCountry();

    List<State> GetState(int country_id);

   List<City> GetCity(int state_id);
}