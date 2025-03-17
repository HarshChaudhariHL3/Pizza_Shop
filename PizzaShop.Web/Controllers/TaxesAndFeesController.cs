using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class TaxesAndFeesController(ITaxesAndFeesService _taxService) : Controller
{
    [HttpGet]
    public IActionResult TaxesAndFees()
    {
        try
        {
            var taxFeesList = _taxService.GetTaxesAndFees();
            var viewModel = new TaxesAndFeesViewModel
            {
                TaxFeesList = taxFeesList.Select(tax => new TaxesAndFeesViewModel
                {
                    TaxId = tax.TaxId,
                    TaxName = tax.TaxName,
                    TaxType = tax.TaxType,
                    TaxValue = tax.TaxValue,
                    IsDefault = tax.IsDefault,
                    IsEnabled = tax.IsEnabled,
                }).ToList()
            };
            return View(viewModel);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    [HttpPost]
    public IActionResult DeleteTaxFee(int taxId)
    {
        try
        {
            _taxService.DeleteTaxFee(taxId);
            TempData["Success"] = "Tax or fee deleted successfully.";
            return RedirectToAction("TaxesAndFees");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    
}
