using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogUserPageVisitActions.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogUserPageVisitActions.Commands.Create;

public class CreatedLogUserPageVisitActionResponse : BaseResponse, IResponse
{
    public GetByGidLogUserPageVisitActionResponse Obj { get; set; }
}