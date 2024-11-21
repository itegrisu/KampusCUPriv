using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Create;

public class CreatedTransportationExternalServiceResponse : BaseResponse, IResponse
{
    public GetByGidTransportationExternalServiceResponse Obj { get; set; }
}