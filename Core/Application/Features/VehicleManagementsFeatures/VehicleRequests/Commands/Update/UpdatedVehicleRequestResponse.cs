using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Update;

public class UpdatedVehicleRequestResponse : BaseResponse, IResponse
{
    public GetByGidVehicleRequestResponse Obj { get; set; }
}