
namespace PizzaShop.Entity.ViewModel;

public class TableOrderAppViewModel
{
    // public int TableId { get; set; }
    // public string TableName { get; set; }
    // public int SectionId { get; set; }
    // public string SectionName { get; set; }
    // public int Capacity { get; set; }
    // public bool Status { get; set; }

    public List<SectionsViewModel> SectionList { get; set; } = new List<SectionsViewModel>();

}
