using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class UserService(IUserRepository _repository) : IUserService
{
    public ProfileViewModel GetUser(string id)
    {
        if (!int.TryParse(id, out int userId))
        {
            return null;
        }
        var user = _repository.GetAll(userId);
        if (user == null)
        {
            return null;
        }

        var profileViewModel = new ProfileViewModel
        {
            Id = user.UserId,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Country = user.CountryId,
            State = user.StateId,
            City = user.CityId,
            Phone = string.Join(", ", user.Phone),
            Address = user.Address,
            ZipCode = user.ZipCode,
        };

        return profileViewModel;
    }

    public List<Country> GetCountries()
    {
        return _repository.GetCountry();
    }

    public List<Role> GetRoles()
    {
        return _repository.GetRoleList();
    }

    public List<State> GetStates(int CountryId)
    {
        return _repository.GetState(CountryId);
    }

    public List<City> GetCities(int StateId)
    {
        return _repository.GetCity(StateId);
    }

    public bool UpdateUser(ProfileViewModel model)
    {
        var user = _repository.GetAll(model.Id);
        if (user == null)
        {
            return false;
        }

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Username = model.Username;
        user.CountryId = model.Country;
        user.StateId = model.State;
        user.CityId = model.City;
        user.Phone = model.Phone;
        user.Address = model.Address;
        user.ZipCode = model.ZipCode;

        return _repository.Update(user);
    }

    // public bool UpdateProfileImage(ProfileViewModel model)
    // {
    //     var user = _repository.GetAll(model.Id);
    //     if (user == null)
    //     {
    //         return false;
    //     }

    //     user.ProfileImg = model.ProfileImg;

    //     return _repository.Update(user);
    // }

    public PaginatedList<UserlistViewModel> pagination_user_list(int page, int pageSize, string search)
    {
        var userlist = new List<User>();
        var count = 0;
        if (string.IsNullOrEmpty(search))
        {
            count = _repository.pagination_count(search);
            userlist = _repository.pagination_user_list(page, pageSize, search);

        }else{
            count = _repository.get_usercount(search);
            userlist = _repository.pagination_user_list(page, pageSize, search);
        }

        List<UserlistViewModel> user_data = new List<UserlistViewModel>();
        foreach (var user in userlist)
        {
            var role = user.UserRole.HasValue ? _repository.GetRole(user.UserRole.Value) : null;
            UserlistViewModel user_view = new UserlistViewModel();

            user_view.FirstName = user.FirstName;
            user_view.LastName = user.LastName;
            user_view.Email = user.Email;
            // user_view.ProfileImg = string.Join(", ", user.ImgUrl);
            user_view.Phone = user.Phone?.ToString();
            user_view.Status = user.Status ?? false;
            // user_view.RoleName = role?.RoleName;

            user_data.Add(user_view);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        return new PaginatedList<UserlistViewModel>(user_data, page, totalPages);
    }

    public bool AddUser(AdduserViewModel model)
    {
        var hashed = BCrypt.Net.BCrypt.HashPassword(model.Password);
        var user = new User
        {
            Email = model.Email,
            UserRole = model.Role,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            Password = hashed,
            CountryId = model.Country,
            StateId = model.State,
            CityId = model.City,
            Phone = model.Phone,
            Address = model.Address,
            ZipCode = model.ZipCode,
            // ProfileImg = model.ProfileImg,
        };

        return _repository.Add(user);
    }
        


}
