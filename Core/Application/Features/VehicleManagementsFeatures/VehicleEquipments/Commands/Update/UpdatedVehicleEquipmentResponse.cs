using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Update;

public class UpdatedVehicleEquipmentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleEquipmentResponse Obj { get; set; }
}