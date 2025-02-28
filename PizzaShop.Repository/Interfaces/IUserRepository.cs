using PizzaShop.Entity.Models;
namespace PizzaShop.Repository.Interfaces;

public interface IUserRepository
{
     User GetAll(int id);

    List<Country> GetCountry();

    List<State> GetState(int country_id);

    Role GetRole(int id);

    List<Role> GetRoleList();

   List<City> GetCity(int state_id);

   List<User> pagination_user_list(int page, int page_size, string search);

   int pagination_count(string search);

    int get_usercount(string search);

    bool Update(User user);

    bool Add(User user);

}