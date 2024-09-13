using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Update;

public class UpdatedCityResponse : BaseResponse, IResponse
{
    public GetByGidCityResponse Obj { get; set; }
}