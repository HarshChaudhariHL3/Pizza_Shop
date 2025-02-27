using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class UserService(IUserRepository _repository) : IUserService
{
    public ProfileViewModel GetUser(int id)
    {
        var user = _repository.GetAll(id);
        if (user == null)
        {
            return null;
        }

        var profileViewModel = new ProfileViewModel
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Country = user.CountryId,
            State = user.StateId,
            City = user.CityId,
            Phone = user.Phone,
            Address = user.Address,
            ZipCode = user.ZipCode,
        };

        return profileViewModel;
    }

    public List<Country> GetCountries()
    {
        return _repository.GetCountry();
    }

    public List<State> GetStates(int CountryId)
    {
        return _repository.GetState(CountryId);
    }

    public List<City> GetCities(int StateId)
    {
        return _repository.GetCity(StateId);
    }

}
