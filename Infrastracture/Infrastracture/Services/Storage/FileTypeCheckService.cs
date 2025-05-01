using Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace Infrastracture.Services.Storage
{
    public class FileTypeCheckService : IFileTypeCheckService
    //Asagidaki CheckFileType metotlarının 2sini de kullanılması lazım.
    {
        private string GetMimeType(IFormFile file)
        {
            string mimeType = null;

            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                byte[] buffer = ms.ToArray();

                if (buffer.Length >= 2 && buffer.Take(2).SequenceEqual(new byte[] { 255, 216 })) // JPG/JPEG
                {
                    mimeType = "image/jpeg";
                }
                else if (buffer.Length >= 8 && buffer.Take(8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 })) // PNG
                {
                    mimeType = "image/png";
                }
                else if (buffer.Length >= 12 && buffer.Take(12).SequenceEqual(new byte[] { 82, 73, 70, 70, 0, 0, 0, 0, 87, 69, 66, 80 })) // WEBP
                {
                    mimeType = "image/webp";
                }
                else if (buffer.Length >= 4 && buffer.Take(4).SequenceEqual(new byte[] { 37, 80, 68, 70 })) // PDF
                {
                    mimeType = "application/pdf";
                }
                else
                {
                    //TODO
                    // Diğer dosya türlerini burada tanımlayabilirsiniz
                }
            }

            return mimeType;
        }
        public async Task<bool> CheckFileType(string extension, IFormFile file)//Dosyanın tipini kontrol eder. MimeType ile
        {
            string mimeType = GetMimeType(file);

            if (mimeType != null && (extension == ".jpeg" || extension == ".jpg") &&
                mimeType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (mimeType != null && extension == ".png" &&
                mimeType.Equals("image/png", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (mimeType != null && extension == ".webp" &&
                mimeType.Equals("image/webp", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (mimeType != null && extension == ".pdf" &&
                mimeType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CheckFileType(string extension, string[] allowedExtensions)//Uzantıya göre kontrol eder. 
        {
            if (!Array.Exists(allowedExtensions, ext => ext.Equals(extension, StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }
            return true;
        }
    }
}
