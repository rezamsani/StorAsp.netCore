using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Command.RemovCategory
{
    public interface IRemovCategory
    {
        ResultDto Execute(long categoryId);
    }
}
