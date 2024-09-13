using Application.Helpers;
using Infrastracture.Helpers.cls;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API.Filters
{
    public class CustomAuthorizationFilter : IAsyncActionFilter
    {
        private readonly GetUserInfo _getUserInfo;
        private readonly clsAuth _clsAuth;

        public CustomAuthorizationFilter(GetUserInfo getUserInfo, clsAuth clsAuth)
        {
            _getUserInfo = getUserInfo;
            _clsAuth = clsAuth;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {


            var userId = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var page = context.HttpContext.Request.Headers["Page"].ToString();


            //if (!await _clsAuth.AuthCheck(userId, page))
            //{
            //    context.Result = new BadRequestObjectResult("/PageAuthError");
            //    return;
            //}

            await next(); // Action metoduna devam et
        }
    }

}
