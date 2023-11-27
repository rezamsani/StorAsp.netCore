using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Orders.Query.GetUserOrder;

namespace EndPoint.SiteMain.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IGetUserOrder _getUserOrder;
        public OrdersController(IGetUserOrder getUserOrder)
        {
            _getUserOrder = getUserOrder;
        }
        public IActionResult Index()
        {
            long userId = ClaimUtility.GetUserId(User).Value;
            return View(_getUserOrder.Execute(userId).Data);
        }
    }
}
