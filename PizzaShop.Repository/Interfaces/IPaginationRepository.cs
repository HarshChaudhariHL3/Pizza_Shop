using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IPaginationRepository
{
    List<User> pagination_user_list(int page, int page_size, string search);
    List<CategoryItem> pagination_category_list(int page, int page_size, string search);

    int get_usercount(string search);
    int get_categoryItem_count(string search);
}
