using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Login
{
    public class LoginUserResponse : BaseResponse, IResponse 
    {
        public Guid UserGid { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
