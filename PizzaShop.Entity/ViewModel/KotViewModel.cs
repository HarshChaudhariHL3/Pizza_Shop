namespace PizzaShop.Entity.ViewModel;

public class KotViewModel
{
    public List<KotModifierGroup> ModifierGroupList { get; set; }

}


public class KotModifierGroup
{
    public int ModifierGroupId { get; set; }

    public string ModifierName { get; set; } = null!;

}
