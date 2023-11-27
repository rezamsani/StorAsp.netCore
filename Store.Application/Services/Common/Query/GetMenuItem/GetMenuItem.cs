using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Common.Query.GetMenuItem
{
    public class GetMenuItem : IGetMenuItem
    {
        private readonly IDataBaseContext _context;
        public GetMenuItem(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<List<MenuItemDto>> Execute()
        {
            var category = _context.Categroys.Include(cat => cat.SubCategorys)
                .Where(cat => cat.ParentCategoryId == null)
                .ToList()
                .Select(cat => new MenuItemDto
                {
                    CatId = cat.Id,
                    Name = cat.Name,
                    Child = cat.SubCategorys.ToList()
                    .Select(ch => new MenuItemDto
                    {
                        CatId = ch.Id,
                        Name = ch.Name
                    }).ToList()
                }).ToList();
            return new ResultDto<List<MenuItemDto>>()
            {
                Data = category,
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
