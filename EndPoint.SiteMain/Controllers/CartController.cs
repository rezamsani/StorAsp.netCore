using EndPoint.Site.Utilities;
using EndPoint.SiteMain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.CartServices;
using System.Linq.Expressions;

namespace EndPoint.SiteMain.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly CookiesManeger _cookiesManeger;
        public CartController(ICartServices cartServices)
        {
            _cartServices = cartServices;
            _cookiesManeger = new CookiesManeger();
        }
        public IActionResult Index()
        {
            var userId = ClaimUtility.GetUserId(User);
            var resutlGetList = _cartServices.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), userId);
            return View(resutlGetList.Data);
        }
        public IActionResult AddToCart(long ProductId)
        {
            CookiesManeger cookiesManeger = new CookiesManeger();
            var resultAdd = _cartServices.AddToCart(ProductId, _cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
        public IActionResult Add(long CartItemId)
        {
            _cartServices.Add(CartItemId);
            return RedirectToAction("Index");
        }
        public IActionResult LowOff(long CartItemId)
        {
            _cartServices.LowOff(CartItemId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(long ProductId)
        {
            _cartServices.RemoveFromCart(ProductId, _cookiesManeger.GetBrowserId(HttpContext));
            return RedirectToAction("Index");
        }
    }
}
