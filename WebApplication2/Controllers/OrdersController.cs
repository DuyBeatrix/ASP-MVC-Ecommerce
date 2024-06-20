using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using WebApplication2.Models.Authentication;
using X.PagedList;

namespace WebApplication2.Controllers
{
    public class OrdersController : Controller
    {
        private OganiContext data = new OganiContext();
        [Authentication]
        public IActionResult Orders(int? page)
        {
            int pageSize = 5;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            int? cusId = HttpContext.Session.GetInt32("cusID");
            var orders = data.Orders.Where(o => o.CusId == cusId).Include(c=>c.Cus).Include(s => s.Ship).Include(p=>p.Payment).OrderByDescending(o=>o.OrderId).ToList();
            PagedList<Order> list = new PagedList<Order>(orders, pageNumber, pageSize);
            return View(list);
        }
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            var order = await data.Orders.Include(s => s.Ship).Include(p => p.Payment).Include(c=>c.Cus).FirstOrDefaultAsync(o => o.OrderId == orderId);
            var orderItems = await data.OrderItems.Include(p => p.Product).Where(o => o.OrderId == orderId).ToListAsync();
            //var orderTotalPrice = await data.Orders
            //                        .Where(o => o.OrderId == orderId)
            //                        .Select(o => o.OrderTotalprice)
            //                        .FirstOrDefaultAsync();

            //var totalReceiptFast = orderTotalPrice - 30000;
            //var totalReceiptNormal = orderTotalPrice - 1500;
            //ViewData["TotalReceiptFast"] = totalReceiptFast;
            //ViewData["TotalReceiptNormal"] = totalReceiptNormal;
            ViewData["Order"] = order;
            ViewData["orderItems"] = orderItems;
            return View();
        }
    }
}
