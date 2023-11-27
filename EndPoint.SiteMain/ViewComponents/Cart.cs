using EndPoint.Site.Utilities;
using EndPoint.SiteMain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.CartServices;

namespace EndPoint.SiteMain.ViewComponents
{
    public class Cart:ViewComponent
    {
        private readonly ICartServices _cartServices;
        private readonly CookiesManeger _cookiemanager;
        public Cart(ICartServices cartServices)
        {
            _cartServices = cartServices;
            _cookiemanager = new CookiesManeger(); // you should Injection settings
        }
        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "Cart", _cartServices.GetMyCart(_cookiemanager.GetBrowserId(HttpContext), userId).Data);
        }
    }
}
