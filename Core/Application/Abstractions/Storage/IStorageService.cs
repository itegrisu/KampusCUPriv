namespace Application.Abstractions.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get; }
        void FileCopy(string FileName, string tempPath, string destinationPath);
    }
}
