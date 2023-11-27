using Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePage.Query.GetSlider
{
    public interface IGetSlider
    {
        ResultDto<List<SliderDto>> Execute();
    }
}
