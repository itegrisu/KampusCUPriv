using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Application.Helpers 
{
    public class GetUserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserIpAddress()
        {
            try
            {
                string ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                if (string.IsNullOrEmpty(ip))
                    return "Empty!";
                return ip;

            }
            catch
            {
                return "Error!";
            }
        }

        public string GetUserSessionID()
        {
            try
            {  //"7de9d9ff-a662-4a40-ba65-57e3fac6dacb"

                string sessionId = _httpContextAccessor.HttpContext.Request.Headers["SessionId"].ToString();
                if (string.IsNullOrEmpty(sessionId))
                    return "Empty!";
                return sessionId;
            }
            catch
            {
                return "Error!";
            }
        }

        public string GetUserGid()
        {
            try
            {
                string gid = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                return gid;
            }
            catch
            {
                return "Error!";
            }
        }

        public string GetPageInfo()
        {
            try
            {
                string pageInfo = _httpContextAccessor.HttpContext.Request.Headers["Page"].ToString();
                return pageInfo;
            }
            catch
            {
                return "Error!";
            }
        }

        public string GetIsSystemAdmin()
        {
            try
            {
                string isSystemAdmin = _httpContextAccessor.HttpContext.User.FindFirst("isSystemAdmin")?.Value;
                return isSystemAdmin;
            }
            catch
            {
                return "Error!";
            }
        }

        public string GetUserEmail()
        {
            try
            {
                string email = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                return email;
            }
            catch
            {
                return "Error!";
            }
        }
    }
}
