namespace Application.Abstractions.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
        void Dispose();
    }
}
