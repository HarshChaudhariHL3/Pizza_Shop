using System.Linq;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class RoleRepository(PizzaShopDbContext _content) : IRoleRepository
{



    public List<Role> GetRoles()
    {
        return _content.Roles.ToList();
    }

    public Role GetRoleById(int roleId)
    {
        var role = _content.Roles.FirstOrDefault(r => r.RoleId == roleId);
        if (role == null)
        {
            throw new Exception($"Role with id {roleId} not found.");
        }
        return role;
    }

    public List<RolePermission> GetPermissionByroleId(int roleId)
    {
        return _content.RolePermissions.Where(rp => rp.RoleId == roleId).ToList();
    }
    public List<Permission> GetPermissionListByRoleId(int roleId)
    {
        var permissionList = _content.RolePermissions
                                    .Where(rp => rp.RoleId == roleId)
                                    .OrderBy(rp => rp.Permission.PermissionId)
                                    .Select(rp => rp.Permission)
                                    .ToList();
        
        return permissionList;
    }



}
