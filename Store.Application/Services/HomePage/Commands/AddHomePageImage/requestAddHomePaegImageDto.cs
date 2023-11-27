using Microsoft.AspNetCore.Http;
using Store.Domain.Entities.HomePage;

namespace Store.Application.Services.HomePage.Commands.AddHomePageImage
{
    public class requestAddHomePaegImageDto
    {
        public IFormFile file { get; set; }
        public string Link { get; set; }
        public ImageLocation  ImageLocation { get; set; }

    }
}
