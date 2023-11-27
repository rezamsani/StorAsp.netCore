using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatterns;

namespace EndPoint.SiteMain.ViewComponents
{
    public class Search : ViewComponent
    {
        private readonly ICommon _common;
        public Search(ICommon common)
        {
            _common = common;
        }
        public IViewComponentResult Invoke()
        {
            return View(viewName: "Search", _common.GetCategorySear.Execute().Data);
        }
    }
}
