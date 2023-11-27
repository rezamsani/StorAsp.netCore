using EndPoint.SiteMain.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.HomePage.FacadPaterns;
using EndPoint.SiteMain.Areas.Admin.Models.ViewModels.HomePage;
using Store.Application.Services.Products.Query.GetProductForSite;

namespace EndPoint.SiteMain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomePageFacad _homePageFacad;
        private readonly IProductForSiteFacad _productForSiteFacad;
        public HomeController(ILogger<HomeController> logger, 
            IHomePageFacad homePageFacad,
            IProductForSiteFacad productForSiteFacad
            )
        {
            _logger = logger;
            _homePageFacad = homePageFacad;
            _productForSiteFacad = productForSiteFacad;
        }

        public IActionResult Index()
        {
            HomePageViewModel homePage = new HomePageViewModel()
            {
                Sliders = _homePageFacad.GetSlider.Execute().Data,
                HomePageImages = _homePageFacad.GetHomePageImage.Execute().Data,
                Laptop = _productForSiteFacad.GetProductForSite.Execute(Ordering.theNewest
                , null, 1, 6, 10003).Data.Products
            };
            return View(homePage);
        }
        public IActionResult testAuth() 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "30"),
                new Claim(ClaimTypes.Name, "Nargesss"),
                new Claim(ClaimTypes.Email, "rezamsanitrrga@gmail.com")

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            HttpContext.SignInAsync(principal, properties);
            var name = User.Identity.Name;
            var email = User.FindFirst(ClaimTypes.Email);
            return Content("testAuth");
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