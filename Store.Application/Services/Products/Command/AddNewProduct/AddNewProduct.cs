using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Store.Application.Interfaces.Contexts;
using Store.Common.Dto;
using Store.Domain.Entities.Products;

namespace Store.Application.Services.Products.Command.AddNewProduct
{
    public class AddNewProduct : IAddNewProduct
    {
        private readonly IDataBaseContext _context;
        private readonly IHostingEnvironment _environment;
        public AddNewProduct(IDataBaseContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _environment = hostingEnvironment;
        }
        public ResultDto Execute(RequestAddNewProductDto request)
        {
            try
            {
                var categoroy = _context.Categroys.Find(request.CategoryId);
                Product product = new Product()
                {
                    Brand = request.Brand,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price,
                    Invertory = request.Inventory,
                    Categroy = categoroy, // Impo
                    Displayed = request.Displayed
                };
                _context.Products.Add(product);
                List<ProductImage> productImages = new List<ProductImage>();
                foreach (var item in request.Images)
                {
                    var uploadResult = UploadFile(item);
                    productImages.Add(new ProductImage
                    {
                        Src = uploadResult.FileNameAddress,
                        Product = product
                    });
                }
                _context.ProductImages.AddRange(productImages);
                List<ProductFeaturs> productFeaturs = new List<ProductFeaturs>();
                foreach (var item in request.Features)
                {
                    productFeaturs.Add(new ProductFeaturs
                    {
                        DisplayName = item.DisplayName,
                        Value = item.Value,
                        Product = product
                    });
                }
                _context.ProductFeatures.AddRange(productFeaturs);
                _context.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "محصول با موفقیت به محصولات فروشگاه اضافه شد"
                };
            }
            catch (Exception ex)
            {

                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "خطا رخ داد ",
                };
            }
        }
        private UploadDto? UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string folder = $@"images/ProductImages/";
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                if(file == null || file.Length == 0)
                {
                    return new UploadDto()
                    {
                        Status = false,
                        FileNameAddress = ""
                    };
                }
                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                var filePath = Path.Combine(uploadsRootFolder, fileName);
                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStram);
                }
                return new UploadDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true
                };
            }
            return null;
        }
    }
}
