using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Products.Command.AddNewProduct
{
    public interface IAddNewProduct
    {
        ResultDto Execute(RequestAddNewProductDto request);
    }
}
