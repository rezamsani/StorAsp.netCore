using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePage.Commands.AddHomePageImage
{
    public interface IAddHomePageImage
    {
        ResultDto Execute(requestAddHomePaegImageDto request);
    }
}
