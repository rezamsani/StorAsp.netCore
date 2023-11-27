using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Common.UploadFile;
using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Commands.AddNewSlider
{
    public class AddNewSlider : IAddNewSlider
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewSlider(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }
       
        public ResultDto Execute(IFormFile form, string Link)
        {
            UploadFileDto resultUpload = UploadFiles.InsertFile(form, _environment, $@"images/HomePage/Slider/");
            Slider slider = new Slider()
            {
                Link = Link,
                Src = resultUpload.FileNameAddress,
            };
            _context.Sliders.Add(slider);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }

}
