using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IPaginationService
{
    PaginatedList<UserlistViewModel> pagination_user_list(int page, int pageSize, string search);
    PaginatedList<CategoryListViewModel> pagination_category_list(int page, int pageSize, string search);
}
