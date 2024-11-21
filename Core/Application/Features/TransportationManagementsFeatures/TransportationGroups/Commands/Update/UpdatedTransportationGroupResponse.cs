using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationGroups.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.TransportationGroups.Commands.Update;

public class UpdatedTransportationGroupResponse : BaseResponse, IResponse
{
    public GetByGidTransportationGroupResponse Obj { get; set; }
}