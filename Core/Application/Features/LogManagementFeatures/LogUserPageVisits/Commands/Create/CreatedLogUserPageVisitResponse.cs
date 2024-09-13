using Application.Features.Base;
using Application.Features.LogManagementFeatures.LogUserPageVisits.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.LogManagementFeatures.LogUserPageVisits.Commands.Create;

public class CreatedLogUserPageVisitResponse : BaseResponse, IResponse
{
    public GetByGidLogUserPageVisitResponse Obj { get; set; }
}