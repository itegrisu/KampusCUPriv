using Application.Abstractions.Storage.Local;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastracture.Services.Storage.Local
{
    public class LocalStorage : BaseStorage, ILocalStorage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public LocalStorage(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public async Task DeleteAsync(string path, string fileName)
        => File.Delete($"{path}\\{fileName}"); //dosyaları siliyor ama ileride has file kullanarak once olup olmadığına bakıp daha sonra silebiliriz

        public List<string> GetFiles(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.GetFiles().Select(x => x.FullName).ToList();// dosyaların ismini getiriyor
        }

        public bool HasFile(string path, string fileName)
        => File.Exists($"{path}\\{fileName}");

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,  path);

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string NewFileName = await FileRenameAsync(path, file.FileName, HasFile);
                await CopyFileAsync($"{uploadPath}\\{NewFileName}", file);
                //datas.Add((NewFileName, $"{_configuration["BaseStorageUrl"]}\\{path}\\{NewFileName}"));
                datas.Add((NewFileName, $"{path}\\{NewFileName}"));

            }
            return datas;
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

    }
}
