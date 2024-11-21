using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Update;

public class UpdatedTransportationResponse : BaseResponse, IResponse
{
    public GetByGidTransportationResponse Obj { get; set; }
}