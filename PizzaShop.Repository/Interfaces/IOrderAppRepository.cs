using PizzaShop.Entity.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IOrderAppRepository
{
    List<ModifierGroup> GetModifierGroupList();
}
