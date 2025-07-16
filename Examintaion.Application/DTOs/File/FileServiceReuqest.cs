using Microsoft.AspNetCore.Http;

namespace Template.Application.DTOs.File
{
    public class FileServiceReuqest
    {
        public IFormFile FilePath { get; set; } = null!;
        public ICollection<string> AllowedExtentions { get; set; } = [];
        public int MaxSizeInBytes { get; set; }
    }
}
