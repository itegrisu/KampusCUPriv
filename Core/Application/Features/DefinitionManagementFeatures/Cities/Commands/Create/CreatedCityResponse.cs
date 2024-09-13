using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.Cities.Commands.Create;

public class CreatedCityResponse : BaseResponse, IResponse
{
    public GetByGidCityResponse Obj { get; set; }
}