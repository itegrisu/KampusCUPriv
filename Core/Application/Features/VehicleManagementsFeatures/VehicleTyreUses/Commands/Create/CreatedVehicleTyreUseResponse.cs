using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Create;

public class CreatedVehicleTyreUseResponse : BaseResponse, IResponse
{
    public GetByGidVehicleTyreUseResponse Obj { get; set; }
}