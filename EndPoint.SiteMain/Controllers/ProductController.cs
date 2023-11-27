using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Query.GetProductForSite;

namespace EndPoint.SiteMain.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductForSiteFacad _productForSiteFacad;
        public ProductController(IProductForSiteFacad productForSiteFacad)
        {
            _productForSiteFacad = productForSiteFacad;
        }
        public IActionResult Index(Ordering ordering,string SearchKey, int Page, int PageSize = 2, long? CatId = null)
        {
            return View(_productForSiteFacad.GetProductForSite.Execute(ordering, SearchKey, Page, PageSize, CatId).Data);
        }
        public IActionResult Detail(long Id)
        {
            return View(_productForSiteFacad.GetProductDetailForSite.Execute(Id).Data);
        }
    }
}
