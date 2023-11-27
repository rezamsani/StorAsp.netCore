using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Common.Query.GetCategorySear
{
    public class GetCategorySear : IGetCategorySear
    {
        private readonly IDataBaseContext _context;
        public GetCategorySear(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<GetCategorySearDto>> Execute()
        {

            var category = _context.Categroys.Where(p => p.ParentCategoryId == null)
                .ToList()
                .Select(p => new GetCategorySearDto
                {
                    CatId = p.Id,
                    CategoryName = p.Name,
                }).ToList();

            return new ResultDto<List<GetCategorySearDto>>()
            {
                Data = category,
                IsSuccess = true,
            };
        }
    }
}
