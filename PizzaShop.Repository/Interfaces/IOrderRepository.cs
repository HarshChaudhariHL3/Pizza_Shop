using PizzaShop.Entity.Models;

namespace PizzaShop.Repository.Interfaces;

public interface IOrderRepository
{
    List<Order> GetAllOrderList();
}
