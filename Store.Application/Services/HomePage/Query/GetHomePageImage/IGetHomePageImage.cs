using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePage.Query.GetHomePageImage
{
    public interface IGetHomePageImage
    {
        ResultDto<List<HomePageImagesDto>> Execute();
    }
}
