using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleRequests.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Create;

public class CreatedVehicleRequestResponse : BaseResponse, IResponse
{
    public GetByGidVehicleRequestResponse Obj { get; set; }
}