using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Query.GetProductForAdmin
{
    public class GetProductForAdmin: IGetProductForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetProductForAdmin(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductForAdminDto> Execute(int Page = 1, int PageSize = 20)
        {
            int rowCount = 0;
            var products = _context.Products
                .Include(p => p.Categroy)
                .ToPaged(Page, PageSize, out rowCount)
                .Select(p => new ProductsFormAdminList_Dto
                {
                    Id = p.Id,
                    Brand = p.Brand,
                    Category = p.Categroy.Name,
                    Description = p.Description,
                    Displayed = p.Displayed,
                    Inventory = p.Invertory,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();
            return new ResultDto<ProductForAdminDto>()
            {
                Data = new ProductForAdminDto()
                {
                    Products = products,
                    CurrentPage = Page,
                    PageSize = PageSize,
                    RowCount = rowCount
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
