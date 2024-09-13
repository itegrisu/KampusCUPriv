using Microsoft.AspNetCore.Hosting;

namespace Persistence.HangfireJobs.FileSystem
{
    public class HangfireRemoveFile
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HangfireRemoveFile(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task RemoveFile()
        {
            string filesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files"); // files klasörünün yolu
            string tempFolderPath = Path.Combine(filesFolderPath, "0temp"); // 0temp klasörünün yolu


            // 0temp klasöründeki tüm dosyalarý al
            string[] files = Directory.GetFiles(tempFolderPath);

            // Her dosya için iþlem yap
            foreach (string file in files)
            {
                File.Delete(file);
            }

        }
    }
}
