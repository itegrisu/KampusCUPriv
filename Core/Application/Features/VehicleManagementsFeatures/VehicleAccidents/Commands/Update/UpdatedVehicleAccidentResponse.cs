using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Update;

public class UpdatedVehicleAccidentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleAccidentResponse Obj { get; set; }
}