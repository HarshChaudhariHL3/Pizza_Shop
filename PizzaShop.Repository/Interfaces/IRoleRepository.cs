using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface IRoleRepository
{
    List<Role> GetRoles();
    List<RolePermission> GetPermissionByroleId(int roleId);
    List<Permission> GetPermissionListByRoleId(int roleId);
    Role GetRoleById(int roleId);

    }