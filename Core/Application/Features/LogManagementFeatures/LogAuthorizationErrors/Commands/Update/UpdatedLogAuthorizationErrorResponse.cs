using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Update;

public class UpdatedLogAuthorizationErrorResponse : BaseResponse, IResponse
{
    public GetByGidLogAuthorizationErrorResponse Obj { get; set; }
}