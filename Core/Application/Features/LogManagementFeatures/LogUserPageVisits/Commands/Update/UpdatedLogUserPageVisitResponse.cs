using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Update;

public class UpdatedLogUserPageVisitResponse : BaseResponse, IResponse
{
    public GetByGidLogUserPageVisitResponse Obj { get; set; }
}