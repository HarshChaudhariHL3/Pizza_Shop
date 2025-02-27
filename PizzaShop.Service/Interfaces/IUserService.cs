

using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IUserService
{
    ProfileViewModel GetUser(int id);

    List<Country> GetCountries();

    List<State> GetStates(int CountryId);

     List<City> GetCities(int StateId);

}
