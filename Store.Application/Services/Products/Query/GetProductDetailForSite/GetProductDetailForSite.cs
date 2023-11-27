using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Query.GetProductDetailForSite
{
    public class GetProductDetailForSite: IGetProductDetailForSite
    {
        private readonly IDataBaseContext _context;
        public GetProductDetailForSite(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ProductDetailForSiteDto> Execute(long Id)
        {
            var product = _context.Products
                .Include(p=> p.Categroy)
                .ThenInclude(p=> p.ParentCategory)
                .Include(p=> p.ProductImage)
                .Include(p=> p.ProductFeaturs)
                .Where(p=> p.Id==Id).FirstOrDefault();
            if(product == null)
            {
                throw new Exception("Product Not Found");
            }
            product.ViewCount++;
            _context.SaveChanges();
            return new ResultDto<ProductDetailForSiteDto>()
            {
                Data = new ProductDetailForSiteDto
                {
                    Brand = product.Brand,
                    Category = $"{product.Categroy.ParentCategory.Name} - {product.Categroy.Name}",
                    Description = product.Description,
                    Id = product.Id,
                    Price = product.Price,
                    Images = product.ProductImage.Select(p => p.Src).ToList(),
                    Features = product.ProductFeaturs.Select(p => new ProductDetailForSite_FeaturesDto
                    {
                        DisplayName = p.DisplayName,
                        Value = p.Value
                    }).ToList(),
                    Title = product.Name
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
