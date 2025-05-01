using Microsoft.EntityFrameworkCore;

namespace Core.Context
{
    public class Context : DbContext
    {
        //public Context(DbContextOptions<Context> options) : base(options)
        //{
        //}

        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}
