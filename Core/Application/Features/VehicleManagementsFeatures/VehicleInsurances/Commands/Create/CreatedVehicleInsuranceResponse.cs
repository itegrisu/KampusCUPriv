using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleInsurances.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Create;

public class CreatedVehicleInsuranceResponse : BaseResponse, IResponse
{
    public GetByGidVehicleInsuranceResponse Obj { get; set; }
}