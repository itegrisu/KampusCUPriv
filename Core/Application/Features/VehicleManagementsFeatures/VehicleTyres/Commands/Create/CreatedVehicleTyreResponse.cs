using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Create;

public class CreatedVehicleTyreResponse : BaseResponse, IResponse
{
    public GetByGidVehicleTyreResponse Obj { get; set; }
}