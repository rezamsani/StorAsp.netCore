using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.Products.Query.GetProductDetailForSite;
using Store.Application.Services.Products.Query.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.FacadePattern
{
    public class ProductForSiteFacad : IProductForSiteFacad
    {
        private readonly IDataBaseContext _context;
        public ProductForSiteFacad(IDataBaseContext context)
        {
            _context = context;
        }
        private IGetProductForSite _productForSite;
        public IGetProductForSite GetProductForSite
        {
            get
            {
                return _productForSite = _productForSite ?? new GetProductForSite(_context);
            }
        }
        private IGetProductDetailForSite _getProductDetailForSite;
        public IGetProductDetailForSite GetProductDetailForSite
        {
            get
            {
                return _getProductDetailForSite = _getProductDetailForSite ?? new GetProductDetailForSite(_context);
            }
        }
    }
}
