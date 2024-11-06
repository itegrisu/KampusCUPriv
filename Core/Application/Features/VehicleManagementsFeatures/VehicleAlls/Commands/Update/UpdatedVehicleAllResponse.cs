using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Update;

public class UpdatedVehicleAllResponse : BaseResponse, IResponse
{
    public GetByGidVehicleAllResponse Obj { get; set; }
}