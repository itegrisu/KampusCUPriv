using Application.Abstractions.EntityServices;
using Application.Abstractions.UnitOfWork;
using Application.Repositories.GeneralManagementRepo.UserRepo;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.GeneralManagements.UserRepo;
using Persistence.Services.EntityServices;
using Persistence.Services.UnitOfWork;
using C = Core.Context;

namespace Persistence
{
    public static class CustomExtensionPersistence
    {
        public static void AddContainerWithDependenciesPersistence(this IServiceCollection services)
        {


            #region User
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();
            #endregion


            services.AddScoped<C.Context, KampusCUContext>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
