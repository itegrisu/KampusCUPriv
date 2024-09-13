using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;

public class CreatedCountryResponse : BaseResponse, IResponse
{
    public GetByGidCountryResponse Obj { get; set; }
}