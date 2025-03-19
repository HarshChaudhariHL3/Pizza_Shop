using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class PaginationService(IPaginationRepository _paginationRepository, IUserRepository _userRepository) : IPaginationService
{
    public PaginatedList<UserlistViewModel> pagination_user_list(int page, int pageSize, string search)
    {
        var userlist = new List<User>();
        var count = 0;
        if (string.IsNullOrEmpty(search))
        {
            count = _paginationRepository.get_usercount(search);
            userlist = _paginationRepository.pagination_user_list(page, pageSize, search);

        }
        else
        {
            count = _paginationRepository.get_usercount(search);
            if(count == 0){
                return null;
            }
            userlist = _paginationRepository.pagination_user_list(page, pageSize, search);
        }

        List<UserlistViewModel> user_data = new List<UserlistViewModel>();
        foreach (var user in userlist)
        {
            var role = user.UserRole.HasValue ? _userRepository.GetRole(user.UserId) : null;
            UserlistViewModel user_view = new UserlistViewModel();

            user_view.FirstName = user.FirstName;
            user_view.LastName = user.LastName;
            user_view.Imgurl = user.ImgUrl;
            user_view.Email = user.Email;
            user_view.Phone = user.Phone?.ToString();
            user_view.Status = user.Status ?? false;
            user_view.RoleName = role?.RoleName;

            user_data.Add(user_view);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        return new PaginatedList<UserlistViewModel>(user_data, page, totalPages ,pageSize );
    }
    public PaginatedList<CategoryListViewModel> pagination_category_list(int page, int pageSize, string search)
    {
        var categoryList = new List<CategoryItem>();
        var count = 0;
        if (string.IsNullOrEmpty(search))
        {
            count = _paginationRepository.get_categoryItem_count(search);
            categoryList = _paginationRepository.pagination_category_list(page, pageSize, search);

        }
        else
        {
            count = _paginationRepository.get_categoryItem_count(search);
            if(count == 0){
                return null;
            }
            categoryList = _paginationRepository.pagination_category_list(page, pageSize, search);
        }

        List<CategoryListViewModel> category_data = new List<CategoryListViewModel>();
        foreach (var user in categoryList)
        {
            // var role = user.UserRole.HasValue ? _userRepository.GetRole(user.UserId) : null;
            CategoryListViewModel category_view = new CategoryListViewModel();

            category_view.CategoryItemId = user.CategoryItemId;
            category_view.ItemName = user.ItemName;
            category_view.CategoryId = user.CategoryId;
            category_view.DefaultTax = user.DefaultTax;
            category_view.Quantity = user.Quantity;
            category_view.Price = user.Price;
            category_view.UnitId = user.UnitId;
            category_view.Description = user.Description;

            // user_view.FirstName = user.FirstName;
            // user_view.LastName = user.LastName;
            // user_view.Imgurl = user.ImgUrl;
            // user_view.Email = user.Email;
            // user_view.Phone = user.Phone?.ToString();
            // user_view.Status = user.Status ?? false;
            // user_view.RoleName = role?.RoleName;

            category_data.Add(category_view);
        }
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);
        return new PaginatedList<CategoryListViewModel>(category_data, page, totalPages ,pageSize );
    }

    
}
