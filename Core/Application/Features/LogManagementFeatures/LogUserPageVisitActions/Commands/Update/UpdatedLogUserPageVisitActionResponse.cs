using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Update;

public class UpdatedLogUserPageVisitActionResponse : BaseResponse, IResponse
{
    public GetByGidLogUserPageVisitActionResponse Obj { get; set; }
}