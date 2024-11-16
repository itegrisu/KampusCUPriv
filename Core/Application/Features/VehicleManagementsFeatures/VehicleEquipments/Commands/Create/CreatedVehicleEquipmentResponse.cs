using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleEquipments.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Create;

public class CreatedVehicleEquipmentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleEquipmentResponse Obj { get; set; }
}