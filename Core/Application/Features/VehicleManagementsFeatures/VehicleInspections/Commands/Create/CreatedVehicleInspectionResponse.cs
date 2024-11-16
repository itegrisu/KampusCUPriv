using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInspections.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Create;

public class CreatedVehicleInspectionResponse : BaseResponse, IResponse
{
    public GetByGidVehicleInspectionResponse Obj { get; set; }
}