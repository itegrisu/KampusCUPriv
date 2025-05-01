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
            string filesFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Files"); // files klas�r�n�n yolu
            string tempFolderPath = Path.Combine(filesFolderPath, "0temp"); // 0temp klas�r�n�n yolu


            // 0temp klas�r�ndeki t�m dosyalar� al
            string[] files = Directory.GetFiles(tempFolderPath);

            // Her dosya i�in i�lem yap
            foreach (string file in files)
            {
                File.Delete(file);
            }

        }
    }
}
