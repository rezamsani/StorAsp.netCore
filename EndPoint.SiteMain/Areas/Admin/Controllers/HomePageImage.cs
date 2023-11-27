using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.HomePage.Commands.AddHomePageImage;

namespace EndPoint.SiteMain.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomePageImage : Controller
    {
        private readonly IHomePageFacad _homePageFacad;
        public HomePageImage(IHomePageFacad homePageFacad)
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
        public IActionResult Add(requestAddHomePaegImageDto request)
        {
            return View(_homePageFacad.AddHomePageImage.Execute(request));
        }
    }
}
