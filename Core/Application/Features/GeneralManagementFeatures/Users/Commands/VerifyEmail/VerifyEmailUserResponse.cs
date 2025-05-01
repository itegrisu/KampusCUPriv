using Application.Features.Base;
using Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.VerifyEmail
{
    public class VerifyEmailUserResponse : BaseResponse, IResponse
    {
        public string Message{ get; set; }
    }
}
