using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleTyreUses.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Update;

public class UpdatedVehicleTyreUseResponse : BaseResponse, IResponse
{
    public GetByGidVehicleTyreUseResponse Obj { get; set; }
}