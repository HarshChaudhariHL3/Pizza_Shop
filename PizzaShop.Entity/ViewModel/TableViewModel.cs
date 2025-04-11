namespace PizzaShop.Entity.ViewModel;

public class TableViewModel
{
     public int TableId { get; set; }

    public int? SectionId { get; set; }

    public string TableName { get; set; } = null!;

    public int? Capacity { get; set; }

    public string? Status { get; set; }

    public List<TableViewModel> TableList { get; set; }

    public PermissionViewModel PermissionList { get; set; }





}
