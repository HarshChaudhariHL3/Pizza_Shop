using Microsoft.EntityFrameworkCore;
using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class TablesAndSectionRepository(PizzaShopDbContext _context) : ITablesAndSectionRepository
{
    
    public List<Section> GetSections()
    {
        return _context.Sections
        .Where(x => x.IsDeleted == false)
        .OrderBy(x => x.SectionId)
        .ToList();
    }

    public List<TableDetail> TableListById(int SectionId)
    {
        var items = _context.TableDetails
            .Where(x => x.SectionId == SectionId && x.IsDeleted == false)
            .OrderBy(x => x.TableId)
            .ToList();
        return items;
    }
    public TableDetail GetTableById(int itemId)
    {
        return _context.TableDetails.FirstOrDefault(x => x.TableId == itemId)!;
    }
    public void AddSection(SectionsViewModel model)
    {
        var section = new Section
        {
            SectionName = model.SectionName,
            Description = model.Description
        };
        _context.Sections.Add(section);
        _context.SaveChanges();
    }
    public Section GetSectionById(int id)
    {
        return _context.Sections.FirstOrDefault(p => p.SectionId == id);
    }

    public void UpdateSection(Section section)
    {
        _context.Sections.Update(section);
        _context.SaveChanges();
    }
    public void DeleteSection(int id)
    {
        var section = _context.Sections.FirstOrDefault(p => p.SectionId == id);

        if (section != null)
        {
            section.IsDeleted = true;
            _context.Sections.Update(section);
            _context.SaveChanges();
        }
    }

        public void AddTable(TableViewModel model){

        var table = new TableDetail
        {
            TableId = model.TableId,
            SectionId = model.SectionId,
            TableName = model.TableName,
            Capacity = model.Capacity
        };
        _context.TableDetails.Add(table);
        _context.SaveChanges();
    }

    public void UpdateTable(TableDetail tableDetail)
    {
        _context.TableDetails.Update(tableDetail);
        _context.SaveChanges();
    }

    public void DeleteTable(int itemId)
    {
        var tableItem = _context.TableDetails.FirstOrDefault(x => x.TableId == itemId);
        if (tableItem != null)
        {
            tableItem.IsDeleted = true;
            _context.TableDetails.Update(tableItem);
            _context.SaveChanges();
        }

    }

    public IEnumerable< RolePermission> GetPermissionByroleId(int roleId)
    {
        return _context.RolePermissions.Include(rp => rp.Role).Include(rp => rp.Permission).Where(rp => rp.RoleId == roleId).ToList();
    }
}
