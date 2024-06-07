using WebApplication2.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repository;
namespace WebApplication2.ViewComponents
{
    public class CateMenuViewComponent: ViewComponent
    {
        private readonly ICateRepo _cate;
        public CateMenuViewComponent(ICateRepo cateRepo)
        {
            _cate = cateRepo;
        }
        public IViewComponentResult Invoke()
        {
            var cate = _cate.GetAllCategories().OrderBy(X => X.CateName);
            return View(cate);
        }
    }
}

