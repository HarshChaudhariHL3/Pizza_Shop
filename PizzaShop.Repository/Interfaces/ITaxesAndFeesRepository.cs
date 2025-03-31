using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Repository.Interfaces;

public interface ITaxesAndFeesRepository
{
    List<TaxesAndFee> GetTaxesAndFees();
     TaxesAndFee GetTaxFeeById(int id);

    void AddTax(TaxesAndFeesViewModel model);
    void UpdateTax(TaxesAndFee taxesAndFee);

     void DeleteTaxFee(int id);
}
