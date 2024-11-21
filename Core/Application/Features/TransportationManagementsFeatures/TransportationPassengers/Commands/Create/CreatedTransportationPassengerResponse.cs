using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Create;

public class CreatedTransportationPassengerResponse : BaseResponse, IResponse
{
    public GetByGidTransportationPassengerResponse Obj { get; set; }
}