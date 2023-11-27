using EndPoint.SiteMain.Areas.Admin.Models.ViewModels.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Command.RemovCategory;

namespace EndPoint.SiteMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Operator")]
    public class CatogoriesController : Controller
    {
        private readonly IProductFacad _productFacad;
        public CatogoriesController(IProductFacad productFacad)
        {
            _productFacad = productFacad;
        }
        public IActionResult List(long? parentId)
        {
            ViewBag.Parents = new SelectList(_productFacad.GetParents.Execute().Data, "Id", "Name");
            return View(_productFacad.GetCategory.Execute(parentId).Data);
        }
        [HttpGet]
        public IActionResult Addnewcategory(long? parentId)
        {
            ViewBag.parentId = parentId;
            return View();
        }
        [HttpPost]
        public IActionResult Addnewcategory(long? parentId, string Name)
        {
            var result = _productFacad.AddNewCategory.Execute(parentId, Name);
            return Json(result);

        }
        [HttpPost]
        public IActionResult DeleteCategory(long categoryId)
        {
            var result = _productFacad.RemovCategory.Execute(categoryId);
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditeCategory(EditeCategoryViewModel request)
        {
            var result = _productFacad.EditeCategory.Execute(request.categoryId, request.categoryName, request.ParentsId);
            return Json(result);
        }

    }
}
