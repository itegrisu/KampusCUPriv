using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;

public class UpdatedTransportationPassengerResponse : BaseResponse, IResponse
{
    public GetByGidTransportationPassengerResponse Obj { get; set; }
}