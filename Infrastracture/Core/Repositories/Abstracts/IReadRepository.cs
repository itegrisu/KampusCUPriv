using Core.Entities;
using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories.Abstracts
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        int Count();
        int CountFull();
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderByDesc = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<IPaginate<T>> GetListAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderByDesc = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default);

        IQueryable<T> GetAll(bool tracking = true);
        T GetByGid(Guid gid, bool tracking = true);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByGidAsync(Guid gid, bool tracking = true);

    }
}
