using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogSuccessedLogins.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Update;

public class UpdatedLogSuccessedLoginResponse : BaseResponse, IResponse
{
    public GetByGidLogSuccessedLoginResponse Obj { get; set; }
}