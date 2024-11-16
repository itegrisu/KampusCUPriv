using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleMaintenances.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Update;

public class UpdatedVehicleMaintenanceResponse : BaseResponse, IResponse
{
    public GetByGidVehicleMaintenanceResponse Obj { get; set; }
}