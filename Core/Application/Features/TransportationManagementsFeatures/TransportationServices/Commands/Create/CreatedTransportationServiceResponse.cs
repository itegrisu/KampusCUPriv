using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;

public class CreatedTransportationServiceResponse : BaseResponse, IResponse
{
    public GetByGidTransportationServiceResponse Obj { get; set; }
}