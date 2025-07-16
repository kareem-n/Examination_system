using Microsoft.AspNetCore.Http;
using Template.Application.DTOs.File;

namespace Template.Application.Interfaces.File
{
    public interface IFileHandlerService
    {
        Task<(string fullPath, string relativePath)> GetFilePaths(IFormFile fileServiceReuqest);
        Task<bool> DeleteFileAsync(string filePath);
        Task<(string msg, bool status)> ValidateFileAsync(FileServiceReuqest fileServiceReuqest);
        Task<bool> SaveFileAsync(string filePath, Stream stream);
    }
}
