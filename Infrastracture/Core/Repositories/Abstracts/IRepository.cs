using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.Abstracts
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
