using Store.Application.Services.Products.Query.GetCategorys;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetParents
{
    public interface IGetParents
    {
        ResultDto<List<ParentCategoryDto>> Execute();
    }
}
