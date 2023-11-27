using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Query.GetProductDetailForAdmin
{
    public class GetProductDetailForAdmin : IGetProductDetailForAdmin
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForAdmin(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ProductDetailForAdmindto> Execute(long Id)
        {
            var products = _context.Products
                .Include(p => p.Categroy)
                .ThenInclude(p => p.ParentCategory)
                .Include(p => p.ProductFeaturs)
                .Include(p => p.ProductImage)
                .Where(p => p.Id == Id)
                .FirstOrDefault();
            return new ResultDto<ProductDetailForAdmindto>()
            {
                Data = new ProductDetailForAdmindto()
                {
                    Brand = products.Brand,
                    Inventory = products.Invertory,
                    Category = GetCategory(products.Categroy),
                    Description = products.Description,
                    Displayed = products.Displayed,
                    Name = products.Name,
                    Price = products.Price,
                    Features = products.ProductFeaturs.ToList().Select(p=> new ProductDetailFeatureDto
                    {
                        Id=p.Id,
                        DisplayName=p.DisplayName,
                        Value=p.Value
                    }).ToList(),
                    Images = products.ProductImage.ToList().Select(p=> new ProductDetailImagesDto
                    {
                        Id=p.Id,
                        Src=p.Src

                    }).ToList()
                },
                IsSuccess = true,
                Message = ""
            };
        }
        private string GetCategory(Categroy categroy)
        {
            string result = categroy.ParentCategory != null ? $"{categroy.ParentCategory.Name} - " : "";
            return result += categroy.Name;
        }
    }
}
