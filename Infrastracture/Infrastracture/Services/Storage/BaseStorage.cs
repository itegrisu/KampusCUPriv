using Microsoft.Extensions.Configuration;

namespace Infrastracture.Services.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class BaseStorage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);

        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod, bool first = true)
        {
            string newFileName = await Task.Run(async () =>
            {
                string extension = Path.GetExtension(fileName);

                IConfiguration configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string allowedFileTypes = configuration["AllowedFileType"];
                string[] allowedExtensions = allowedFileTypes.Split(", ");

                if (!Array.Exists(allowedExtensions, ext => ext.Equals(extension.TrimStart('.'), StringComparison.OrdinalIgnoreCase)))
                {
                    throw new Exception("The file extension is not among the allowed types.");
                }

                string newFileName = string.Empty;
                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{Guid.NewGuid()}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.IndexOf("-");
                    if (indexNo1 == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo1;
                            indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                            if (indexNo1 == -1)
                            {
                                indexNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1)
                                                .Insert(indexNo1 + 1, _fileNo.ToString());
                        }
                        else
                            newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";

                    }
                }

                //if (LogFile.Exists($"{path}\\{newFileName}"))
                if (hasFileMethod(pathOrContainerName, newFileName))
                    return await FileRenameAsync(pathOrContainerName, newFileName, hasFileMethod, false);
                else
                    return newFileName;
            });

            return newFileName;
        }
    }
}
