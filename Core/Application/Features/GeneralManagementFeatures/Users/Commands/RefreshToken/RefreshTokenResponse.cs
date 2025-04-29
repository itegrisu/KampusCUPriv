using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
