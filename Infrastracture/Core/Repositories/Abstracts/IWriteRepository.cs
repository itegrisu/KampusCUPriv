using Core.Entities;

namespace Core.Repositories.Abstracts
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Add Operations

        bool Add(T model);
        bool AddRange(List<T> models);
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);

        #endregion


        #region Update Operations

        bool Update(T model);
        bool UpdateRange(List<T> models);

        #endregion


        #region Remove Operations

        bool Remove(T model);

        bool RemoveRange(List<T> models);

        Task<bool> RemoveAsync(Guid Gid);

        #endregion

        void Save();
        Task<int> SaveAsync();
    }
}
