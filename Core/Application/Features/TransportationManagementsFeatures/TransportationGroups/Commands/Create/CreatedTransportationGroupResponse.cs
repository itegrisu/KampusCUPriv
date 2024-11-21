using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Create;

public class CreatedTransportationGroupResponse : BaseResponse, IResponse
{
    public GetByGidTransportationGroupResponse Obj { get; set; }
}