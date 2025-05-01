using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Storage
{
    public interface IFileTypeCheckService
    {
        public Task<bool> CheckFileType(string extension, IFormFile file);
        public Task<bool> CheckFileType(string extension, string[] allowedExtensions);
    }
}
