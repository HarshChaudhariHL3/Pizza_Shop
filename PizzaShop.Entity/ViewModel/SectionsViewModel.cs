using PizzaShop.Entity.Models;

namespace PizzaShop.Entity.ViewModel;

public class SectionsViewModel
{
    public int SectionId { get; set; }

    public string SectionName { get; set; } = null!;

    public string? Description { get; set; }

    public List<SectionsViewModel> SectionList { get; set; }
    public List<TableViewModel> TableList { get; set; }
    public List<PermissionViewModel> PermissionList { get; set; }



}
