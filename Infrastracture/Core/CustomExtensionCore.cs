using Core.Persistence.Paging;
using Core.Repositories.Abstracts;
using Core.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CustomExtensionCore
    {
        public static void AddContainerWithDependenciesCore(this IServiceCollection services /* ILogger logger*/)
        {

            #region 
            services.AddScoped(typeof(IPaginate<>), typeof(Paginate<>));
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            #endregion
            //services.AddSingleton(logger);


        }
    }
}
