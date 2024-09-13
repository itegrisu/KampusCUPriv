using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogAuthorizationErrors.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogAuthorizationErrors.Commands.Create;

public class CreatedLogAuthorizationErrorResponse : BaseResponse, IResponse
{
    public GetByGidLogAuthorizationErrorResponse Obj { get; set; }
}