using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Admins.Commands.Login
{
    public class LoginAdminResponse : BaseResponse, IResponse 
    {
        public Guid ClubGid{ get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
