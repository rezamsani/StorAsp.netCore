using Store.Application.Services.HomePage.Query.GetHomePageImage;
using Store.Application.Services.HomePage.Query.GetSlider;
using Store.Application.Services.Products.Query.GetProductForSite;

namespace EndPoint.SiteMain.Areas.Admin.Models.ViewModels.HomePage
{
    public class HomePageViewModel
    {
        public List<SliderDto> Sliders { get; set; }
        public List<HomePageImagesDto> HomePageImages { get; set; }
        public List<ProductForSiteDto> Camera { get; set; }
        public List<ProductForSiteDto> Mobile { get; set; }
        public List<ProductForSiteDto> Laptop { get; set; }
    }
}
