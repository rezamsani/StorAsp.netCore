using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Finance.Query.GetRequestPayForAdmin;

namespace EndPoint.SiteMain.Areas.Admin.Controllers
{
    [Area("admin")]
    public class RequestPayController : Controller
    {
        private readonly IGetRequestPayForAdmin _getRequestPayForAdmin;
        public RequestPayController(IGetRequestPayForAdmin getRequestPayForAdmin)
        {
            _getRequestPayForAdmin = getRequestPayForAdmin;
        }
        public IActionResult Index()
        {
            return View(_getRequestPayForAdmin.Execute().Data);
        }
    }
}
