using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class OrderAppService(IOrderAppRepository _orderAppRepository) : IOrderAppService
{
}
