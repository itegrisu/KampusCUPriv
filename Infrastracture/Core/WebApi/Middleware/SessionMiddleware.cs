using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebApi.Middleware
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // JWT do�ruland�ktan sonra
            if (context.User.Identity.IsAuthenticated)
            {
                // Session ID'yi kontrol et
                var sessionId = _httpContextAccessor.HttpContext.Session.GetString("SessionId");

                if (string.IsNullOrEmpty(sessionId))
                {
                    // Session ID yoksa yeni bir tane olu�tur ve sakla
                    sessionId = Guid.NewGuid().ToString();
                    _httpContextAccessor.HttpContext.Session.SetString("SessionId", sessionId);
                }

                // Session ID'yi loglama i�lemlerinde kullanmak i�in context'e ekleyin
                context.Items["SessionId"] = sessionId;
            }

            await _next(context);
        }
    }

}
