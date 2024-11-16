using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Update;

public class UpdatedVehicleInsuranceResponse : BaseResponse, IResponse
{
    public GetByGidVehicleInsuranceResponse Obj { get; set; }
}