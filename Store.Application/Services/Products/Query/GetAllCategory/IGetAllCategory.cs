using Microsoft.EntityFrameworkCore.Storage;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Query.GetAllCategory
{
    public interface IGetAllCategory
    {
        ResultDto<List<AllCategoryDto>> Execute();
    }
}
