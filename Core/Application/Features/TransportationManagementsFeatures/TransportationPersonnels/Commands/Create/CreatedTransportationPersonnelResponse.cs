using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.TransportationPersonnels.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.TransportationPersonnels.Commands.Create;

public class CreatedTransportationPersonnelResponse : BaseResponse, IResponse
{
    public GetByGidTransportationPersonnelResponse Obj { get; set; }
}