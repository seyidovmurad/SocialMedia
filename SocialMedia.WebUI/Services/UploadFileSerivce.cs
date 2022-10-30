using Microsoft.AspNetCore.Hosting;

namespace SocialMedia.WebUI.Services
{
    public static class UploadFileSerivce
    {
        public static async Task<string> UploadImageAsync(IFormFile file, IWebHostEnvironment webHostEnvironment, string path = "media")
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, path);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return uniqueFileName;
        }

        public static void DeleteImage(IWebHostEnvironment webHostEnvironment, string? fileName, string path)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;
            path = Path.Combine(webHostEnvironment.WebRootPath, path);
            string filePath = Path.Combine(path, fileName);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
