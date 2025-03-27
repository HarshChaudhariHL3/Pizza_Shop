using ClosedXML.Excel;
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

    [Route("/Order/ExportToExcel")]
    [HttpGet]
    public async Task<IActionResult> ExportToExcel()
    {
        var employees = _orderService.GetAllOrderDetailToExport();
        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add("Order");



        worksheet.Cell(1, 1).Value = "Order ID";
        worksheet.Cell(1, 2).Value = "Date";
        worksheet.Cell(1, 3).Value = "Name";
        worksheet.Cell(1, 4).Value = "Order Status";
        worksheet.Cell(1, 5).Value = "Payment Mode";
        worksheet.Cell(1, 6).Value = "Rating";
        worksheet.Cell(1, 7).Value = "Amount";


        int row = 2;
        foreach (var e in employees)
        {
            worksheet.Cell(row, 1).Value = e.OrderId;
            worksheet.Cell(row, 2).Value = e.CreatedAt.ToString();
            worksheet.Cell(row, 3).Value = e.Customer.CustomerName;
            worksheet.Cell(row, 4).Value = e.Status;
            worksheet.Cell(row, 5).Value = e.Payment.PaymentMethod;
            worksheet.Cell(row, 6).Value = e.Feedbacks.FirstOrDefault()?.Rating;
            worksheet.Cell(row, 7).Value = e.TotalAmount;
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        var content = stream.ToArray();
        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Order.xlsx");
    }
}
