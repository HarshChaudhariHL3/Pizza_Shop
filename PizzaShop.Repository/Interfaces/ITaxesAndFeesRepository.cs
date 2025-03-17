using PizzaShop.Entity.Models;

namespace PizzaShop.Repository.Interfaces;

public interface ITaxesAndFeesRepository
{
    List<TaxesAndFee> GetTaxesAndFees();
     TaxesAndFee GetTaxFeeById(int id);

     void DeleteTaxFee(int id);
}
