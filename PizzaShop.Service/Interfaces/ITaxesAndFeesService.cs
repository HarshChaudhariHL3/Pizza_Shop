using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;

namespace PizzaShop.Service.Interfaces;

public interface ITaxesAndFeesService
{
    List<TaxesAndFee> GetTaxesAndFees();
    void AddTax(TaxesAndFeesViewModel model);

     void DeleteTaxFee(int id);

     TaxesAndFeesViewModel GetAllTaxByTaxId(int id);
     void UpdateTax(TaxesAndFeesViewModel model);
}
