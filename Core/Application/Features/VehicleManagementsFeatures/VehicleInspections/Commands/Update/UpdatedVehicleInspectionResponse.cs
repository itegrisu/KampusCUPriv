using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Update;

public class UpdatedVehicleInspectionResponse : BaseResponse, IResponse
{
    public GetByGidVehicleInspectionResponse Obj { get; set; }
}