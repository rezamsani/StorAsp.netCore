using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.HomePage.Query.GetHomePageImage
{
    public class GetHomePageImage : IGetHomePageImage
    {
        private readonly IDataBaseContext _context;
        public GetHomePageImage(IDataBaseContext context)
        {
            _context = context;
        }       
        ResultDto<List<HomePageImagesDto>> IGetHomePageImage.Execute()
        {
            var images = _context.HomePageImages.OrderByDescending(p => p.Id)
                .Select(p => new HomePageImagesDto()
                {
                    Id = p.Id,
                    ImageLocation = p.ImageLocation,
                    Link = p.Link,
                    Src = p.Src
                }).ToList();
            return new ResultDto<List<HomePageImagesDto>>()
            {
                Data = images,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
