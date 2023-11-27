using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.HomePage.Query.GetSlider
{
    public class GetSlider : IGetSlider
    {
        private readonly IDataBaseContext _context;
        public GetSlider(IDataBaseContext context)
        {
            _context = context;

        }
        public ResultDto<List<SliderDto>> Execute()
        {
            var sliders = _context.Sliders.OrderByDescending(slid => slid.Id).ToList()
                .Select(slid => new SliderDto
                {
                    Link = slid.Link,
                    Src = slid.Src
                }).ToList();
            return new ResultDto<List<SliderDto>>()
            {
                Data = sliders,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
