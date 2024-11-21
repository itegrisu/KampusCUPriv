using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationServices.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;

public class UpdatedTransportationServiceResponse : BaseResponse, IResponse
{
    public GetByGidTransportationServiceResponse Obj { get; set; }
}