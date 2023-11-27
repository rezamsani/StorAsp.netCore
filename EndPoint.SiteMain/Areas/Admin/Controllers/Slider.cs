using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.HomePage.Commands.AddNewSlider;

namespace EndPoint.SiteMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Slider : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public Slider(IHomePageFacad homePageFacad)
        {
            _homePageFacad = homePageFacad;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(IFormFile file, string link)
        {
            _homePageFacad.AddNewSlider.Execute(file, link);
            return View();
        }
    }
}
