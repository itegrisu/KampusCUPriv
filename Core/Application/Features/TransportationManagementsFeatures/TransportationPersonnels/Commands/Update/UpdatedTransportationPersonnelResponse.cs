using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Update;

public class UpdatedTransportationPersonnelResponse : BaseResponse, IResponse
{
    public GetByGidTransportationPersonnelResponse Obj { get; set; }
}