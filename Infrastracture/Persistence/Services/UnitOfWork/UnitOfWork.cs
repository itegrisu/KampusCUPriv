using Application.Abstractions.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Context;

namespace Persistence.Services.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KampusCUContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(KampusCUContext context)
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
            _transaction.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
            _transaction = null;
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
