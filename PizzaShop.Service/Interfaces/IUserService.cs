

using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IUserService
{
    ProfileViewModel GetUser(string id);

    EdituserViewModel GetUserByEmail(string Email);

    bool UpdateUser(ProfileViewModel model);

    bool UpdateUserinEdit(EdituserViewModel model);

    bool DeleteUser(string Email);
    

    bool AddUser(AdduserViewModel model);

    List<Role> GetRoles();
    List<Country> GetCountries();

    List<State> GetStates(int CountryId);

    List<City> GetCities(int StateId);


}
