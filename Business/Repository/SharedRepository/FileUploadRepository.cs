using System;
using System.IO;
using System.Threading.Tasks;
using Business.IRepository.ISharedRepository;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;

namespace Business.Repository.SharedRepository
{
    public class FileUploadRepository : IFileUploadRepository
    {
        private readonly IWebHostEnvironment hostEnvironment;
        public FileUploadRepository(IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;

        }
        public async Task<string> UploadImage(string folder, IBrowserFile file)
        {
            FileInfo fileInfo = new(file.Name);
            var fileName = Path.GetFileNameWithoutExtension(file.Name) + Guid.NewGuid().ToString() + fileInfo.Extension;
            var folderDir = $"{hostEnvironment.WebRootPath}\\Images\\{folder}";
            var path = Path.Combine(hostEnvironment.WebRootPath, "Images", folder, fileName);
            MemoryStream memoryStream = new();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            if (!Directory.Exists(folderDir))
            {
                Directory.CreateDirectory(folderDir);
            }
            await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fs);
            }
            var url = $"Images/{folder}/{fileName}";
            return url;
        }

        public void DeleteImage(string folder, string file)
        {
            var path = $"{hostEnvironment.WebRootPath}\\Images\\{folder}\\{file}";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}