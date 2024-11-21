using Application.Features.Base;
using Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.TransportationManagementFeatures.Transportations.Commands.Create;

public class CreatedTransportationResponse : BaseResponse, IResponse
{
    public GetByGidTransportationResponse Obj { get; set; }
}