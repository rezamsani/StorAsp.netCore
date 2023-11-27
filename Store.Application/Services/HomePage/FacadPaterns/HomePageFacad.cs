using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Application.Interfaces.FacadPatterns;
using Store.Application.Services.HomePage.Commands.AddHomePageImage;
using Store.Application.Services.HomePage.Commands.AddNewSlider;
using Store.Application.Services.HomePage.Query.GetHomePageImage;
using Store.Application.Services.HomePage.Query.GetSlider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.HomePage.FacadPaterns
{
    public class HomePageFacad:IHomePageFacad
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public HomePageFacad(IDataBaseContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        private IAddNewSlider _addNewSlider;
        public IAddNewSlider AddNewSlider
        {
            get
            {
                return _addNewSlider = _addNewSlider ?? new AddNewSlider(_context, _environment);
            }
        }
        private IGetSlider _getSlider;
        public IGetSlider GetSlider
        {
            get
            {
                return _getSlider = _getSlider ?? new GetSlider(_context);
            }
        }
        private IAddHomePageImage _addHomePageImage;
        public IAddHomePageImage AddHomePageImage
        {
            get
            {
                return _addHomePageImage = _addHomePageImage ?? new AddHomePageImage(_context, _environment);
            }
        }
        private IGetHomePageImage _getHomePageImage;
        public IGetHomePageImage GetHomePageImage
        {
            get
            {
                return _getHomePageImage = _getHomePageImage ?? new GetHomePageImage(_context);
                     
            }
        }
    }
}
