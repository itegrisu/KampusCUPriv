using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleAlls.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Create;

public class CreatedVehicleAllResponse : BaseResponse, IResponse
{
    public GetByGidVehicleAllResponse Obj { get; set; }
}