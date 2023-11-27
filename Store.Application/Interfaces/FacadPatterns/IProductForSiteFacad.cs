using Store.Application.Services.Products.Query.GetProductDetailForSite;
using Store.Application.Services.Products.Query.GetProductForSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface IProductForSiteFacad
    {
        IGetProductForSite GetProductForSite { get; }
        IGetProductDetailForSite GetProductDetailForSite { get; }
    }
}
