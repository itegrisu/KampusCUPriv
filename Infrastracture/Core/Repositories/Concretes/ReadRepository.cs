using Core.Entities;
using Core.Persistence.Paging;
using Core.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories.Concretes
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly Context.Context _context;

        public ReadRepository(Context.Context context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public int Count()
        {
            return Table.Where(i => i.DataState == Enum.DataState.Active).Count();
        }
        public int CountFull()
        {
            return Table.Count();
        }
        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query.Where(x => x.DataState == Enum.DataState.Active);
        }

        public T GetByGid(Guid gid, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query.FirstOrDefault(x => x.DataState == Enum.DataState.Active && x.Gid == gid);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable().Where(x => x.DataState == Enum.DataState.Active);
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(method);
        }

        public async Task<T> GetByGidAsync(Guid gid, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(model => model.DataState == Enum.DataState.Active && model.Gid == gid);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<T> queryable = Table.AsQueryable().Where(T => T.DataState == Enum.DataState.Active);
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            if (include != null)
            {
                queryable = include(queryable);
            }

            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }

            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderByDesc = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<T> queryable = Table.AsQueryable().Where(entity => entity.DataState == Enum.DataState.Active);
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            if (include != null)
            {
                queryable = include(queryable);
            }

            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            }

            if (orderByDesc != null)
            {
                return await orderByDesc(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            }

            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }
        public async Task<IPaginate<T>> GetListAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderByDesc = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<T> queryable = Table.AsQueryable();
            if (!enableTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            if (include != null)
            {
                queryable = include(queryable);
            }

            if (withDeleted)
            {
                queryable = queryable.IgnoreQueryFilters();
            }

            if (predicate != null)
            {
                queryable = queryable.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            }

            if (orderByDesc != null)
            {
                return await orderByDesc(queryable).ToPaginateAsync(index, size, 0, cancellationToken);
            }

            return await queryable.ToPaginateAsync(index, size, 0, cancellationToken);
        }

    }
}
