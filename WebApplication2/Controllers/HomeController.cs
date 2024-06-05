using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication2.Models;
using X.PagedList;

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

        public IActionResult Index(int ? page)
        {
            int pageSize = 8;
            int pageNumber = page == null ? 1 : page.Value;
            var listProduct = db.Products.AsNoTracking().OrderBy(x =>x.ProductName);
            PagedList<Product> list = new PagedList<Product>(listProduct, pageNumber, pageSize);
            return View(listProduct);
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
