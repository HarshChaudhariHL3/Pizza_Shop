using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaShop.Entity.ViewModel;
using PizzaShop.Service.Interfaces;

namespace PizzaShop.Web.Controllers;

public class OrderController(IOrderService _orderService) : Controller
{
    [HttpGet]
    public IActionResult Orders()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderDetails( int page, int pageSize, string search, string orderStatus, string orderTime,DateTime? fromDate, DateTime? toDate)
    {
        try
        {
            PaginationViewModel<OrderViewModel> table = await _orderService.GetOrderDetail(page, pageSize, search, orderStatus,orderTime,fromDate,toDate);
             return PartialView("./PartialView/_OrderPartial", table);
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
