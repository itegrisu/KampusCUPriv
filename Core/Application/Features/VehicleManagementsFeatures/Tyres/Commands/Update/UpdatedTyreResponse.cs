using Application.Features.Base;
using Application.Features.VehicleManagementFeatures.Tyres.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Update;

public class UpdatedTyreResponse : BaseResponse, IResponse
{
    public GetByGidTyreResponse Obj { get; set; }
}