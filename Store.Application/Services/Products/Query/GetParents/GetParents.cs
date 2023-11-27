using Store.Application.Interfaces.Contexts;
using Store.Application.Services.Products.Query.GetCategorys;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Query.GetParents
{
    public class GetParents : IGetParents
    {
        private readonly IDataBaseContext _context;
        public GetParents(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<ParentCategoryDto>> Execute()
        {
            var categoris = _context.Categroys.Where(cat => cat.ParentCategoryId == null).ToList()
            .Select(cat => new ParentCategoryDto
            {
                Id = cat.Id,
                Name = cat.Name
            }).ToList();
            if(categoris.Count() == 0)
            {
                return new ResultDto<List<ParentCategoryDto>>
                {
                    Data = categoris,
                    IsSuccess = false,
                    Message = "دسته بندی وجود ندارد"
                };
            }
            return new ResultDto<List<ParentCategoryDto>>
            {
                Data = categoris,
                IsSuccess = true,
                Message = "لیست با موفقیت برگشت داده شد"
            };
        }
    }
}
