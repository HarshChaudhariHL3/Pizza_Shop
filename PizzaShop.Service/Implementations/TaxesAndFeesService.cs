using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class TaxesAndFeesService(ITaxesAndFeesRepository _taxRepository) : ITaxesAndFeesService
{


    public List<TaxesAndFee> GetTaxesAndFees()
    {
        return _taxRepository.GetTaxesAndFees();
    }

    public void DeleteTaxFee(int id)
    {
        var taxFee = _taxRepository.GetTaxFeeById(id);
        if (taxFee == null)
        {
            throw new KeyNotFoundException($"Tax or Fee with ID {id} not found.");
        }
        _taxRepository.DeleteTaxFee(id);
    }
}
