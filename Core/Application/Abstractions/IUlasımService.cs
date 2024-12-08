namespace Application.Abstractions
{
    public interface IUlasımService
    {
        Task TestService();
        Task<string> IpListesiAsync();
        // Task<string> SeferEkleAsync(UlasimSeferleri ulasimSeferi)
    }
}
