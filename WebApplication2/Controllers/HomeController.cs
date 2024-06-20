using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Models;
using X.PagedList;
using WebApplication2.Models.ProductDetailViewModel;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        OganiContext db = new OganiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authentication]
        public IActionResult Index(int ? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listProduct = db.Products.AsNoTracking();
            PagedList<Product> lst = new PagedList<Product>(listProduct, pageNumber, pageSize);
            return View(lst);
        }

        public IActionResult ProductByCate(int CateID)
        {
            List<Product> list = db.Products.Where(x=>x.CateId == CateID).ToList();
            return View(list);
        }

        public IActionResult ProductSellQuantity()
        {
            List<Product> list = db.Products.OrderByDescending(x => x.ProductSellquantity).Take(2).ToList();
            return View(list);
        }

        public IActionResult NewProducts()
        {
            DateOnly fiveDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-5));
            List<Product> newProducts = db.Products.Where(x => x.ProductAdd >= fiveDaysAgo).OrderByDescending(x => x.ProductAdd).ToList();
            return View(newProducts);
        }

        public IActionResult Search(string query)
        {
            List<Product> products = db.Products.Where(p => p.ProductName.Contains(query)).ToList();
            return View(products);
        }

        public IActionResult ProductDetail(int ProductId)
        {
            var product = db.Products.SingleOrDefault(x => x.ProductId == ProductId);

            var relatedProducts = db.Products.Where(x => x.CateId == product.CateId && x.ProductId != ProductId).Take(4).ToList();

            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts
            };

            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
