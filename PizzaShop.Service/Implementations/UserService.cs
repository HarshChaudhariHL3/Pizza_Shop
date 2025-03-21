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

        var role = user.UserRole.HasValue ? _repository.GetRole(user.UserId) : null;

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
            Phone = user.Phone,
            Address = user.Address,
            ZipCode = user.ZipCode,
            Role = role?.RoleName,
            Imgurl = user.ImgUrl
        };

        return profileViewModel;
    }

    public EdituserViewModel GetUserByEmail(string Email)
    {
        var user = _repository.GetAllByEmail(Email);
        if (user == null)
        {
            return null;
        }

        var role = user.UserRole.HasValue ? _repository.GetRole(user.UserRole.Value) : null;

        var edituserViewModel = new EdituserViewModel
        {
            Email = user.Email,
            Role = user.UserRole,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Username = user.Username,
            Status = user.Status,
            Country = user.CountryId,
            State = user.StateId,
            City = user.CityId,
            Phone = user.Phone,
            Address = user.Address,
            ZipCode = user.ZipCode,
            ImgUrl = user.ImgUrl
        };

        return edituserViewModel;
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
        user.ImgUrl = model.Imgurl;
        user.Address = model.Address;
        user.ZipCode = model.ZipCode;

        return _repository.Update(user);
    }
    public bool UpdateUserinEdit(EdituserViewModel model)
    {
        if (model != null)
        {

            var user = _repository.GetAllByEmail(model.Email);
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
            user.Status = model.Status;
            user.UserRole = model.Role;
            user.ImgUrl = model.ImgUrl;

            return _repository.Update(user);
        }
        return false;
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


    public bool AddUser(AdduserViewModel model)
    {
        if (model != null)
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
                Status = model.Status,
                ImgUrl = model.Imgurl
            };

            return _repository.Add(user);
        }
        return false;
    }

    public bool DeleteUser(string Email)
    {
        var user = _repository.GetAllByEmail(Email);
        if (user == null)
        {
            return false;
        }

        return _repository.Delete(user);
    }


    // public async  Task<PaginationViewModel<UserlistViewModel>> GetUserList(string searchUser, string sort, int page, int pageSize){
        
    //     List<User> userList =  _repository.GetAllUser();
    //     List<UserlistViewModel> users = new List<UserlistViewModel>();


    //     foreach(User user in userList){
    //        users.Add(new UserlistViewModel{
    //         FirstName = user.FirstName,
    //         LastName = user.LastName,
    //         Email = user.Email,
    //         RoleId = user.UserRole,
    //         RoleName =user.UserRole.HasValue ? _repository.GetRoleName(user.UserRole.Value) : null,
    //         Phone = user.Phone,
    //         Status = user.Status,
    //         Imgurl = user.ImgUrl,
    //         UserId = user.UserId,
    //        });

    //     }
    //     if(!string.IsNullOrEmpty(searchUser)){
    //         users = users.Where(u => (u.FirstName + ' ' + u.LastName).Contains(searchUser)).ToList();
    //     }
    //     users = sort switch{
    //         "name_desc" => users.OrderByDescending(u => (u.FirstName + ' ' + u.LastName).Contains(searchUser)).ToList(),
    //         "name_asc" => users.OrderBy(u => (u.FirstName + ' ' + u.LastName).Contains(searchUser)).ToList(),
    //         "role_desc" => users.OrderByDescending(u => u.RoleName).ToList(),
    //         "role_asc" => users.OrderBy(u => u.RoleName).ToList(),
    //         _ => users.OrderBy(u => u.FirstName).ToList(),
    //     };

    //     int usersCount = users.Count;
    //     users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
    //     return new PaginationViewModel<UserlistViewModel>
    //     {
    //         Items = users,
    //         TotalItems = usersCount,
    //         CurrentPage = page,
    //         PageSize = pageSize,
    //     };
    // }
}
