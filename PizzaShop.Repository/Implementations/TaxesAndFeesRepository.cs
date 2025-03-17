using PizzaShop.Entity.Data;
using PizzaShop.Entity.Models;
using PizzaShop.Repository.Interfaces;

namespace PizzaShop.Repository.Implementations;

public class TaxesAndFeesRepository(PizzaShopDbContext _context) : ITaxesAndFeesRepository
{
    public List<TaxesAndFee> GetTaxesAndFees()
    {
        return _context.TaxesAndFees.ToList();
    }

    public TaxesAndFee GetTaxFeeById(int id){

        var tax =  _context.TaxesAndFees.FirstOrDefault(c => c.TaxId == id);

        return tax;
    }

    public void DeleteTaxFee(int id){
        var tax = _context.TaxesAndFees.FirstOrDefault(x => x.TaxId == id);
        if(tax !=null){
            _context.TaxesAndFees.Remove(tax);
            _context.SaveChanges();
        }
    }
}
