using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetProductDetailForAdmin
{
    public interface IGetProductDetailForAdmin
    {
        ResultDto<ProductDetailForAdmindto> Execute(long Id);
    }
}
