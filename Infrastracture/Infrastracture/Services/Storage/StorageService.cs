using Application.Abstractions.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastracture.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StorageService(IStorage storage, IWebHostEnvironment webHostEnvironment)
        {
            _storage = storage;
            _webHostEnvironment = webHostEnvironment;
        }

        public string StorageName => _storage.GetType().Name;

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
         => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public void FileCopy(string FileName, string tempPath, string destinationPath)
        {
            string tempFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, tempPath); // 0temp klasörünün yolu
            string avatarFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, destinationPath); // user-avatara klasörünün yolu

            if (!Directory.Exists(avatarFolderPath))
                Directory.CreateDirectory(avatarFolderPath);

            string sourceFile = Path.Combine(tempFolderPath, FileName);
            string destinationFile = Path.Combine(avatarFolderPath, FileName);

            File.Copy(sourceFile, destinationFile, true); // true parametresi, hedef dosyanın varsa üzerine yazılmasını sağlar
        }

        public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
        => _storage.UploadAsync(pathOrContainerName, files);

    }
}
