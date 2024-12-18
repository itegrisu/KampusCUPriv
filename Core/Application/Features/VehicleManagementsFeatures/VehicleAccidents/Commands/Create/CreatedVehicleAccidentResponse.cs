using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.VehicleAccidents.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Create;

public class CreatedVehicleAccidentResponse : BaseResponse, IResponse
{
    public GetByGidVehicleAccidentResponse Obj { get; set; }
}