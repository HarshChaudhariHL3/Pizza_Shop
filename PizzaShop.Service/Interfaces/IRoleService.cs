using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface IRoleService
{
    List<Role> GetRoles();
    Role GetRoleById(int roleId);

    List<RolePermission> GetPermissionByroleId(int roleId);
    List<Permission> GetPermissionListByRoleId(int roleId);

    RoleViewModel UpdatePermission (RoleViewModel model);
}
