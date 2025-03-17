using PizzaShop.Entity.Models;

namespace PizzaShop.Service.Interfaces;

public interface ITaxesAndFeesService
{
    List<TaxesAndFee> GetTaxesAndFees();

     void DeleteTaxFee(int id);
}
