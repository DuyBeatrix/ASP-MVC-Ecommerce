using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;
using X.PagedList;

namespace WebApplication2.Controllers
{
    public class ShopController : Controller
    {
        OganiContext db = new OganiContext();
        public IActionResult ViewShop(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.Products.AsNoTracking();
            PagedList<Product> lst = new PagedList<Product>(listProduct, pageNumber, pageSize);
            return View(lst);
        }
        //public IActionResult ProductByCateShop(int CateID)
        //{
        //    List<Product> list = db.Products.Where(x => x.CateId == CateID).ToList();
        //    return View(list);
        //}
        public IActionResult ProductByCateShop(int CateID, int? page)
        {
            int pageSize = 3;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.Products.Where(x => x.CateId == CateID).AsNoTracking();
            ViewBag.CateID = CateID;
            IPagedList<Product> lst = new PagedList<Product>(listProduct, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult ProductSellQuantityShop()
        {
            List<Product> list = db.Products.Where(x => x.ProductSellquantity > 5).OrderByDescending(x => x.ProductSellquantity).ToList();
            return View(list);
        }

        public IActionResult NewProductsShop()
        {
            DateOnly fiveDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-5));
            List<Product> newProducts = db.Products.Where(x => x.ProductAdd >= fiveDaysAgo).OrderByDescending(x => x.ProductAdd).ToList();
            return View(newProducts);
        }

        public IActionResult SearchShop(string query)
        {
            List<Product> products = db.Products.Where(p => p.ProductName.Contains(query)).ToList();
            return View(products);
        }
        public IActionResult SearchByPrice(decimal minPrice, decimal maxPrice)
        {
            List<Product> products = db.Products.Where(p => p.ProductPrice >= minPrice*1000 && p.ProductPrice <= maxPrice*1000).ToList();
            return View(products);
        }
    }
}
