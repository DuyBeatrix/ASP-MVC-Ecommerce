using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Models;
using WebApplication2.ViewModels;
using X.PagedList;
using WebApplication2.Models.Authentication;

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
            var listProduct = db.Products.ToList();
            
            return View(listProduct);
        }

        public IActionResult ProductByCate(int CateID)
        {
            List<Product> list = db.Products.Where(x=>x.CateId == CateID).ToList();
            return View(list);
        }

        public IActionResult ProductDetail(int ProductId)
        {
            var product = db.Products.SingleOrDefault(x => x.ProductId == ProductId);
            
            return View(product);
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
