using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Create;

public class CreatedVehicleMaintenanceResponse : BaseResponse, IResponse
{
    public GetByGidVehicleMaintenanceResponse Obj { get; set; }
}