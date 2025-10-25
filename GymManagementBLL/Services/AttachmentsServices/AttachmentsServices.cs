using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;



namespace GymManagementBLL.Services.AttachmentsServices
{
    public class AttachmentsServices : IAttachmentsServices
    {
        private readonly string[] allowedExtentions = { ".jpg", ".jpeg", ".png" };
        private readonly long maxFileSize = 5 * 1024 * 1024;
        private readonly IHostingEnvironment _webHost;

        public AttachmentsServices(IHostingEnvironment webHost)
        {
            _webHost = webHost;
        }


        public async Task<string>? Upload(string FolderName, IFormFile file)
        {
            try
            {
                if (file is null || FolderName is null || file.Length == 0)
                    throw new Exception("No file uploaded");
                var extention = Path.GetExtension(file.FileName).ToLower();
                if (!allowedExtentions.Contains(extention))
                    throw new Exception("No file uploaded");

                var folderPath = Path.Combine(_webHost.WebRootPath, "images", FolderName);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var fileName = $"{Guid.NewGuid}{extention}";
                var filePath = Path.Combine(folderPath, fileName);

                using var filestream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(filestream);
                return fileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed To upload File To Folder = {FolderName} + {ex}");
                return null!;
            }

        }

        public bool Delete(string FolderName, string FileName)
        {
            string filePath = Path.Combine(_webHost.WebRootPath, "images", FolderName, FileName);

            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return true;
        }
    }
}
