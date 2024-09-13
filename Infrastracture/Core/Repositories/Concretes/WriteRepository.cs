using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Core.Repositories.Abstracts;

namespace Core.Repositories.Concretes
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly Context.Context _context;

        public WriteRepository(Context.Context context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        #region Add Operations

        public bool Add(T model)
        {

            EntityEntry entityEntry = Table.Add(model);
            return entityEntry.State == EntityState.Added;
        }

        public bool AddRange(List<T> models)
        {
            Table.AddRange(models);
            return true;
        }

        public async Task<bool> AddAsync(T model)
        {

            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);
            return true;
        }

        #endregion


        #region Update Operations

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> models)
        {
            Table.UpdateRange(models);
            return true;
        }

        #endregion



        #region Remove Operations

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);

            return true;
        }

        public async Task<bool> RemoveAsync(Guid gid)
        {
            T model = await Table.FirstOrDefaultAsync(model => model.Gid == gid);

            return Remove(model);

        }

        #endregion


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
