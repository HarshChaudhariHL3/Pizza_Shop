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

    
}
