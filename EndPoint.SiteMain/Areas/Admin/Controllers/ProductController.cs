using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Command.AddNewProduct;

namespace EndPoint.SiteMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductFacad _productFacad;
        public ProductController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult Index(int Page=1, int PageSize= 20)
        {
            return View(_productFacad.GetProductForAdmin.Execute(Page, PageSize).Data);
        }
        public IActionResult detail(long Id)
        {
            return View(_productFacad.GetProductDetailForAdmin.Execute(Id).Data);
        }
        [HttpGet]
        public IActionResult AddNewProduct()
        {
            ViewBag.Categories = new SelectList(_productFacad.GetAllCategory.Execute().Data, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult AddNewProduct(RequestAddNewProductDto request, List<AddNewProductFeaturs> Featurs)
        {
            List<IFormFile> images = new List<IFormFile>();
            for (int i = 0; i < Request.Form.Files.Count(); i++)
            {
                var file = Request.Form.Files[i];
                images.Add(file);
            }
            request.Images = images;
            request.Features = Featurs;
            return Json(_productFacad.AddNewProduct.Execute(request));
        }
    }
}
