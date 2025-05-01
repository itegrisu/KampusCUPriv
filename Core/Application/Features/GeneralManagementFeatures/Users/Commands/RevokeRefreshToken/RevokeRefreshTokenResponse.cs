using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenResponse
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public bool IsValid { get; set; }
    }
}
