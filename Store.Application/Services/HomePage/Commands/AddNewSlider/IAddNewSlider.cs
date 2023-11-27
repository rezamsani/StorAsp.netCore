using Microsoft.AspNetCore.Http;
using Store.Application.Services.Products.Command.AddNewProduct;
using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePage.Commands.AddNewSlider
{
    public interface IAddNewSlider
    {
        ResultDto Execute(IFormFile form, string Link);
    }

}
