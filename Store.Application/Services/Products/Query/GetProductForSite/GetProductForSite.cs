using Microsoft.EntityFrameworkCore;
using Store.Application.Interfaces.Contexts;
using Store.Common;
using Store.Common.Dto;

namespace Store.Application.Services.Products.Query.GetProductForSite
{
    public class GetProductForSite: IGetProductForSite
    {
        private readonly IDataBaseContext _context;
        public GetProductForSite(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultProductForSiteDto> Execute(Ordering ordering, string SearchKey, int pageNumber, int pageSize, long? CatId = null)
        {
            int totalRow = 0;
            Random rd = new Random();
            var productQuery = _context.Products.Include(pro => pro.ProductImage)
                .AsQueryable();
            if(CatId != null)
            {
                productQuery = productQuery
                .Where(p => p.CategoryId == CatId || p.Categroy.ParentCategoryId==CatId)
                .AsQueryable();
            }
            if (!string.IsNullOrWhiteSpace(SearchKey))
            {
                productQuery = productQuery.Where(p => p.Name.Contains(SearchKey) || p.Brand.Contains(SearchKey)).AsQueryable();
            }
            switch (ordering)
            {
                case Ordering.NotOrder:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostVisited:
                    productQuery = productQuery.OrderByDescending(p => p.ViewCount).AsQueryable();
                    break;
                case Ordering.Bestselling:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.MostPopular:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.theNewest:
                    productQuery = productQuery.OrderByDescending(p => p.Id).AsQueryable();
                    break;
                case Ordering.Cheapest:
                    productQuery = productQuery.OrderBy(p => p.Price).AsQueryable();
                    break;
                case Ordering.theMostExpensive:
                    productQuery = productQuery.OrderByDescending(p => p.Price).AsQueryable();
                    break;
                default:
                    break;
            }

            var products = productQuery.ToPaged(pageNumber, pageSize, out totalRow);
            return new ResultDto<ResultProductForSiteDto>()
            {
                Data = new ResultProductForSiteDto()
                {
                    TotalRow = totalRow,
                    Products = products.Select(pro => new ProductForSiteDto
                    {
                        Id = pro.Id,
                        Star = rd.Next(1, 5),
                        ImageSrc = pro.ProductImage.FirstOrDefault().Src,
                        Price = pro.Price,
                        Title = pro.Name
                    }).ToList(),
                },
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
