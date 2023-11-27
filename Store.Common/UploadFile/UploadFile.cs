using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.UploadFile
{
    public static class UploadFiles
    {
        public static UploadFileDto? InsertFile(IFormFile file, IHostingEnvironment _environment, string MyPathFile)
        {
            if (file != null)
            {
                string folder = MyPathFile;
                var uploadsRootFolder = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadsRootFolder))
                {
                    Directory.CreateDirectory(uploadsRootFolder);
                }
                if (file == null || file.Length == 0)
                {
                    return new UploadFileDto()
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
                return new UploadFileDto()
                {
                    FileNameAddress = folder + fileName,
                    Status = true
                };
            }
            return null;
        }        
    }
    public class UploadFileDto
    {
        public long Id { get; set; }
        public bool Status { get; set; }
        public string FileNameAddress { get; set; }
    }
}
