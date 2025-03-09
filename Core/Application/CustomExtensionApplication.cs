using Application.Helpers;
using Application.Helpers.PaginationHelpers;
using Application.Helpers.UpdateRowNo;
using Core.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class CustomExtensionApplication
    {
        public static void AddContainerWithDependenciesApplication(this IServiceCollection services)
        {


            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CustomExtensionApplication).Assembly));
            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            #region Helpers
            services.AddScoped(typeof(NoPagination<,>));
            services.AddScoped(typeof(UpdateRowNoHelper<,>));
            #endregion

            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPushNotificationService, FirebasePushNotificationService>();

            // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
   )
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (Type? item in types)
                if (addWithLifeCycle == null)
                    services.AddScoped(item);
                else
                    addWithLifeCycle(services, type);
            return services;
        }
    }
}
