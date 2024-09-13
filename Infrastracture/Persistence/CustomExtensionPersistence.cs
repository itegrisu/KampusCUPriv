using Application.Abstractions.EntityServices;
using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Application.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Application.Repositories.GeneralManagementRepos.UserRepo;
using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Application.Repositories.PortalManagementRepos.PortalParameterRepo;
using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using Core.Repositories.Abstracts;
using Core.Repositories.Concretes;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.AuthManagementRepos.AuthPageRepo;
using Persistence.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Persistence.Repositories.AuthManagementRepos.AuthRoleRepo;
using Persistence.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Persistence.Repositories.GeneralManagementRepos.UserRepo;
using Persistence.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Persistence.Repositories.LogManagementRepos.LogEmailSendRepo;
using Persistence.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Persistence.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Persistence.Repositories.PortalManagementRepos.PortalParameterRepo;
using Persistence.Repositories.PortalManagementRepos.PortalTextRepo;
using Persistence.Services.EntityServices;
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

            #region UserRefreshToken 
            services.AddScoped<IUserRefreshTokenReadRepository, UserRefreshTokenReadRepository>();
            services.AddScoped<IUserRefreshTokenWriteRepository, UserRefreshTokenWriteRepository>();
            #endregion


            //#region Country
            //services.AddScoped<ICountryReadRepository, CountryReadRepository>();
            //services.AddScoped<ICountryWriteRepository, CountryWriteRepository>();
            //#endregion


            #region logRepo
            services.AddScoped<ILogUserPageVisitActionReadRepository, LogUserPageVisitActionReadRepository>();
            services.AddScoped<ILogUserPageVisitActionWriteRepository, LogUserPageVisitActionWriteRepository>();

            services.AddScoped<ILogUserPageVisitWriteRepository, LogUserPageVisitWriteRepository>();
            services.AddScoped<ILogUserPageVisitReadRepository, LogUserPageVisitReadRepository>();

            services.AddScoped<ILogFailedLoginReadRepository, LogFailedLoginReadRepository>();
            services.AddScoped<ILogFailedLoginWriteRepository, LogFailedLoginWriteRepository>();

            services.AddScoped<ILogAuthorizationErrorReadRepository, LogAuthorizationErrorReadRepository>();
            services.AddScoped<ILogAuthorizationErrorWriteRepository, LogAuthorizationErrorWriteRepository>();

            services.AddScoped<ILogSuccessedLoginReadRepository, LogSuccessedLoginReadRepository>();
            services.AddScoped<ILogSuccessedLoginWriteRepository, LogSuccessedLoginWriteRepository>();
            #endregion


            #region Auth Repo
            services.AddScoped<IAuthPageReadRepository, AuthPageReadRepository>();
            services.AddScoped<IAuthPageWriteRepository, AuthPageWriteRepository>();

            services.AddScoped<IAuthRolePageReadRepository, AuthRolePageReadRepository>();
            services.AddScoped<IAuthRolePageWriteRepository, AuthRolePageWriteRepository>();

            services.AddScoped<IAuthRoleReadRepository, AuthRoleReadRepository>();
            services.AddScoped<IAuthRoleWriteRepository, AuthRoleWriteRepository>();

            services.AddScoped<IAuthUserRoleReadRepository, AuthUserRoleReadRepository>();
            services.AddScoped<IAuthUserRoleWriteRepository, AuthUserRoleWriteRepository>();

            #endregion

            #region Portal Parameter 
            services.AddScoped<IPortalParameterReadRepository, PortalParameterReadRepository>();
            services.AddScoped<IPortalParameterWriteRepository, PortalParameterWriteRepository>();
            #endregion


            #region Portal Text
            services.AddScoped<IPortalTextReadRepository, PortalTextReadRepository>();
            services.AddScoped<IPortalTextWriteRepository, PortalTextWriteRepository>();
            #endregion

            #region Base
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            #endregion

            #region Log Email Send
            services.AddScoped<ILogEmailSendReadRepository, LogEmailSendReadRepository>();
            services.AddScoped<ILogEmailSendWriteRepository, LogEmailSendWriteRepository>();
            #endregion


            services.AddScoped<C.Context, Emasist2024Context>();
            services.AddScoped<IDatabaseService, DatabaseService>();
            services.AddScoped<ILogService, LogService>();


        }
    }
}
