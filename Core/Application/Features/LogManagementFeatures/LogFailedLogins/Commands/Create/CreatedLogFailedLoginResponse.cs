using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Create;

public class CreatedLogFailedLoginResponse : BaseResponse, IResponse
{
    public GetByGidLogFailedLoginResponse Obj { get; set; }
}