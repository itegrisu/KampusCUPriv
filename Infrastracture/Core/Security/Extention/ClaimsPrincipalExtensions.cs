using System.Collections.Immutable;
using System.Security.Claims;

namespace Core.Security.Extention
{
    public static class ClaimsPrincipalExtensions
    {
        public static ICollection<string>? Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal?.FindAll(claimType)?.Select((Claim x) => x.Value).ToImmutableArray();
        }

        public static ICollection<string>? ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.Claims("http://schemas.microsoft.com/ws/2008/06/identity/claims/role");
        }

        public static object GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        }
    }
}
