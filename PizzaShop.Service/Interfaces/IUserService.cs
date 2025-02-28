

using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IUserService
{
    ProfileViewModel GetUser(string id);

    bool UpdateUser(ProfileViewModel model);

    bool AddUser(AdduserViewModel model);

    List<Role> GetRoles();
    List<Country> GetCountries();

    List<State> GetStates(int CountryId);

    List<City> GetCities(int StateId);

    PaginatedList<UserlistViewModel> pagination_user_list(int page, int pageSize, string search);

}
