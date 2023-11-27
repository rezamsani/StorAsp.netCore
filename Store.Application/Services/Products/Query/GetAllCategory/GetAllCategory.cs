using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Query.GetAllCategory
{
    public class GetAllCategory : IGetAllCategory
    {
        private readonly IDataBaseContext _context;
        public GetAllCategory(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<AllCategoryDto>> Execute()
        {
            var categoris = _context.Categroys.Include(cat => cat.ParentCategory)
                .Where(cat => cat.ParentCategoryId != null).ToList()
                .Select(cat => new AllCategoryDto
                {
                    Id = cat.Id,
                    Name = $"{cat.ParentCategory.Name}-{cat.Name}"
                }).ToList();
            return new ResultDto<List<AllCategoryDto>>()
            {
                Data = categoris,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
