using Microsoft.AspNetCore.Hosting;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Common.UploadFile;
using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Commands.AddHomePageImage
{
    public class AddHomePageImage : IAddHomePageImage
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddHomePageImage(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }
        public ResultDto Execute(requestAddHomePaegImageDto request)
        {
            var resultUpload = UploadFiles.InsertFile(request.file, _environment, $@"images/HomePage/MainPage/");
            HomePageImages homePageImages = new HomePageImages()
            {
                Link = request.Link,
                Src = resultUpload.FileNameAddress,
                ImageLocation=request.ImageLocation,
            };
            _context.HomePageImages.Add(homePageImages);
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
