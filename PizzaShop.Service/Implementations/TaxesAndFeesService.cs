using PizzaShop.Entity.Models;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Service.Implementations;

public class TaxesAndFeesService(ITaxesAndFeesRepository _taxRepository) : ITaxesAndFeesService
{


    // public List<TaxesAndFee> GetTaxesAndFees()
    // {
    //     return _taxRepository.GetTaxesAndFees();
    // }
    public List<TaxesAndFee> GetTaxesAndFees(string search = "")
    {
        var query = _taxRepository.GetTaxesAndFees(); // Fetch all records

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(t => t.TaxName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                     t.TaxType.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return query.OrderBy(x => x.TaxId).ToList(); // Optional: to keep the sorting
    }

    public void AddTax(TaxesAndFeesViewModel model)
    {
        _taxRepository.AddTax(model);
    }
    public void UpdateTax(TaxesAndFeesViewModel model)
    {
        var taxes = _taxRepository.GetTaxFeeById(model.TaxId);
        if (taxes != null)
        {
            taxes.TaxName = model.TaxName;
            taxes.TaxValue = model.TaxValue;
            taxes.TaxType = model.TaxType;
            taxes.IsDefault = model.IsDefault;
            taxes.IsEnabled = model.IsEnabled;
            _taxRepository.UpdateTax(taxes);
        }
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

    public TaxesAndFeesViewModel GetAllTaxByTaxId(int id)
    {
        var item = _taxRepository.GetTaxFeeById(id);
        if (item == null)
            return null;

        return new TaxesAndFeesViewModel
        {
            TaxName = item.TaxName,
            TaxType = item.TaxType,
            IsDefault = item.IsDefault,
            IsEnabled = item.IsEnabled,
            TaxValue = item.TaxValue
        };
    }
}