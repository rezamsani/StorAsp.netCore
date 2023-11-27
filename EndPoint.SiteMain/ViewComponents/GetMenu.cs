using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.SiteMain.ViewComponents
{
    public class GetMenu:ViewComponent
    {
        private readonly ICommon _common;
        public GetMenu(ICommon common)
        {
            _common = common;
        }
        public IViewComponentResult Invoke()
        {
            var menuItem = _common.GetMenuItem.Execute();
            return View(viewName: "GetMenu", menuItem.Data);

        }
    }
}
