using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogFailedLogins.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogFailedLogins.Commands.Update;

public class UpdatedLogFailedLoginResponse : BaseResponse, IResponse
{
    public GetByGidLogFailedLoginResponse Obj { get; set; }
}