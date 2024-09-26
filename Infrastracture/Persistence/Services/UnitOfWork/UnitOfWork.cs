using Application.Abstractions.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;

namespace Persistence.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Emasist2024Context _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(Emasist2024Context context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _transaction?.Dispose();
        }
    }

}
