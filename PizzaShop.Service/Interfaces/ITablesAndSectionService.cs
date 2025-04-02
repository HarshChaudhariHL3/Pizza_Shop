using Microsoft.AspNetCore.Http;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface ITablesAndSectionService
{
    List<Section> GetSections();
    Task<PaginationViewModel<TableViewModel>> GetTable(int SectionId, int page, int pageSize, string search = "");

    void AddSection(SectionsViewModel model);
    void EditSection(SectionsViewModel model);
    bool DeleteSection(int itemId);
     void AddTable(TableViewModel model);
     TableViewModel GetTableById(int itemId);
     void EditTable(TableViewModel model);
     bool DeleteTable(int itemId);

    IEnumerable<RolePermission> GetPermissionByroleId(int roleId);

     bool DeleteMultipleTable(List<int> dataId);
}
