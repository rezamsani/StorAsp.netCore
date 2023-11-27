using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Command.EditeCategory
{
    public interface IEditeCategory
    {
        ResultDto Execute(long categoryId, string categoryName, long ParentsId);
    }
}
