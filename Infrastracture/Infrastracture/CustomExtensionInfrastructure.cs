using Application.Abstractions;
using Application.Abstractions.Auth;
using Application.Abstractions.Storage;
using Application.Abstractions.Token;
using Application.Helpers;
using Infrastracture.Helpers.cls;
using Infrastracture.Services;
using Infrastracture.Services.Auth;
using Infrastracture.Services.LogUserPageVisit;
using Infrastracture.Services.Storage;
using Infrastracture.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastracture
{
    public static class CustomExtensionInfrastructure
    {
        public static void AddContainerWithDependenciesInfrastucture(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<clsAuth>();
            services.AddScoped<GetUserInfo>();
            services.AddScoped<LogUserPageVisitService>();
            services.AddScoped<IFileTypeCheckService, FileTypeCheckService>();
            services.AddScoped<IMailService, MailService>();

        }

        public static void AddStorage<T>(this IServiceCollection services) where T : BaseStorage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
