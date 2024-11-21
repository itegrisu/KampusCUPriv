using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Update;

public class UpdatedTransportationExternalServiceResponse : BaseResponse, IResponse
{
    public GetByGidTransportationExternalServiceResponse Obj { get; set; }
}