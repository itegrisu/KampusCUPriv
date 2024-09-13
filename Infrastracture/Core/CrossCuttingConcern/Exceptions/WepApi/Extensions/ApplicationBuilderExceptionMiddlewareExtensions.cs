using Core.CrossCuttingConcern.Exceptions.WepApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.CrossCuttingConcern.Exceptions.WepApi.Extensions
{
    public static class ApplicationBuilderExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>(Array.Empty<object>());
        }
    }
}
