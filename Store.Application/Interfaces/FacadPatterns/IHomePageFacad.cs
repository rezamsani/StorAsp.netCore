using Store.Application.Services.HomePage.Commands.AddHomePageImage;
using Store.Application.Services.HomePage.Commands.AddNewSlider;
using Store.Application.Services.HomePage.Query.GetHomePageImage;
using Store.Application.Services.HomePage.Query.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Interfaces.FacadPatterns
{
    public interface IHomePageFacad
    {
        
        IAddNewSlider AddNewSlider { get; }
        IGetSlider GetSlider { get; }
        IAddHomePageImage AddHomePageImage { get; }
        IGetHomePageImage GetHomePageImage { get; }



    }
}
