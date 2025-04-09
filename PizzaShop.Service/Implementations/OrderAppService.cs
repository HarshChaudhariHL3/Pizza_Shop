using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class OrderAppService(IOrderAppRepository _orderAppRepository) : IOrderAppService
{
    public KotViewModel GetModifierGroupList()
    {
        var modifierGroupList = _orderAppRepository.GetModifierGroupList();

        var modifierGroupViewModelList = new KotViewModel
        {
            ModifierGroupList = new List<KotModifierGroup>()
        };
        foreach (var modifierGroup in modifierGroupList)
        {
            var modifierGroupViewModel = new KotModifierGroup
            {
                ModifierGroupId = modifierGroup.ModifierGroupId,
                ModifierName = modifierGroup.ModifierName
            };
            modifierGroupViewModelList.ModifierGroupList.Add(modifierGroupViewModel);
        }

        return  modifierGroupViewModelList;
    }
}
