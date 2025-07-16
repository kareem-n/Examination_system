using Microsoft.AspNetCore.Http;
using Template.Application.DTOs.File;
using Template.Application.Interfaces.File;

namespace Template.Application.Services.FileHanlder
{
    public class FileHandlerService : IFileHandlerService
    {
        public readonly string _basePath;

        public FileHandlerService()
        {
            _basePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public Task<bool> DeleteFileAsync(string filePath)
        {
            throw new NotImplementedException();
        }

        public async Task<(string fullPath, string relativePath)> GetFilePaths(IFormFile file)
        {
            var fileName = GenerateFileName(file.FileName);
            var relativePath = Path.Combine("uploads", fileName);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

            return (fullPath, relativePath);
        }

        public async Task<(string msg, bool status)> ValidateFileAsync(FileServiceReuqest fileServiceReuqest)
        {
            if (fileServiceReuqest.FilePath == null || fileServiceReuqest.FilePath.Length == 0)
            {
                return ("File can not be empty", false);
            }
            if (fileServiceReuqest.MaxSizeInBytes > 0 && fileServiceReuqest.FilePath.Length > fileServiceReuqest.MaxSizeInBytes)
            {
                return ($"File Size is too large to '{fileServiceReuqest.MaxSizeInBytes * 1024} MB'", false);
            }
            if (fileServiceReuqest.AllowedExtentions != null && fileServiceReuqest.AllowedExtentions.Count > 0)
            {
                var fileExtension = Path.GetExtension(fileServiceReuqest.FilePath.FileName);
                if (!fileServiceReuqest.AllowedExtentions.Contains(fileExtension))
                {
                    return ("File Extension not allowed", false);
                }
            }
            return (null!, true);
        }

        private string GenerateFileName(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            var newFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyyMMddHHmmssfff}{fileExtension}";
            return newFileName;
        }

        public async Task<bool> SaveFileAsync(string filePath, Stream stream)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (directory != null && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
                return true;
            }
        }
    }
}
