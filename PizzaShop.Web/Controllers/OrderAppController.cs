using Microsoft.AspNetCore.Mvc;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;


namespace PizzaShop.Web.Controllers;

public class OrderAppController(IOrderAppService _orderAppService) : Controller
{
    #region Kot
    public IActionResult Kot()
    {
        var CategoryList = _orderAppService.GetCategoryList();
        return View(CategoryList);
    }

    // public IActionResult KotOrders(int categoryId , string progress){
    //     var orderDetails = _orderAppService.GetKOTCardDetails(categoryId, progress);
    //     return PartialView("_KotModifierItem", orderDetails);
    // }


    #endregion

    #region Menu
    public IActionResult MenuOrderApp()
    {
        return View();
    }

    #endregion
    #region Tables
    public IActionResult Tables()
    {
        var TableList = _orderAppService.GetTableList();
        return View(TableList);
    }

    public IActionResult AddWaitingUser (){
        return PartialView("./PartialView/_TableWaitingListModal");
    }

    [HttpGet]
    public IActionResult SectionsList()
    {
        var sectionsList = _orderAppService.GetSections();

        return Json(sectionsList);
    }

    [HttpPost]
    public IActionResult AddWaitingList(WaitingListViewModel model){
         try
        {
            _orderAppService.AddWaitingList(model);
            TempData["Success"] = "Added To Waiting List Successfully";
            return RedirectToAction("Tables");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
    #endregion
    #region WaitingList
    public IActionResult WaitingList()
    {
        return View();
    }
    #endregion

}
