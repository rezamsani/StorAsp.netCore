using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetProductDetailForSite
{
    public interface IGetProductDetailForSite
    {
        ResultDto<ProductDetailForSiteDto> Execute(long Id);
    }
}
