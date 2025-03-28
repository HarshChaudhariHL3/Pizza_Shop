// Code to implement a custom action filter to check user permissions for specific actions
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaShop.Service.Interfaces;
using PizzaShop.Entity.Models;

public class PermissionFilter : IActionFilter
{
    private readonly IRoleService _roleService;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionFilter(IRoleService roleService, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
    {
        _roleService = roleService;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var userRoleId = 0;
        var principal = _jwtService.ValidateToken(_httpContextAccessor.HttpContext.Request.Cookies["SuperSecretAuthToken"]);
        if (principal == null)
        {
            context.Result = new RedirectResult("/Validation/Login");
            return;
        }
        if (principal != null)
        {
            userRoleId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");
        }

        
        var rolePermissions = _roleService.GetPermissionByroleId(userRoleId);


        var actionName = context.ActionDescriptor.RouteValues["controller"];

        bool hasPermission = CheckUserPermission(actionName, rolePermissions);

        if (!hasPermission)
        {
            
            context.Result = new ForbidResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var userRoleId = 0;
        var principal = _jwtService.ValidateToken(_httpContextAccessor.HttpContext.Request.Cookies["SuperSecretAuthToken"]);
        if (principal != null)
        {
            userRoleId = int.Parse(principal.FindFirst("UserId")?.Value ?? "0");
        }

        var rolePermissions = _roleService.GetPermissionByroleId(userRoleId);
        var actionName = context.ActionDescriptor.RouteValues["controller"];

        bool canAddEdit = rolePermissions.Any(rp =>
            rp.Permission != null &&
            rp.Permission.ModuleName == actionName &&
            rp.CanAddEdit);

        bool CanDelete = rolePermissions.Any(rp =>
            rp.Permission != null &&
            rp.Permission.ModuleName == actionName &&
            rp.CanDelete);

        if (!canAddEdit)
        {
            context.HttpContext.Items["CanAddEdit"] = canAddEdit;
        }
        if (!CanDelete)
        {
            context.HttpContext.Items["CanDelete"] = CanDelete;
        }
    }

    private bool CheckUserPermission(string actionName, List<RolePermission> rolePermissions)
    {
        // Check permissions for CanView, CanAddEdit, and CanDelete
        return rolePermissions.Any(rp =>
            rp.Permission != null &&
            rp.Permission.ModuleName == actionName &&
            (rp.CanView || rp.CanAddEdit || rp.CanDelete));
    }
}

