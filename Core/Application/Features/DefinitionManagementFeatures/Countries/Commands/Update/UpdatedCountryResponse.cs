using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.Countries.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Update;

public class UpdatedCountryResponse : BaseResponse, IResponse
{
    public GetByGidCountryResponse Obj { get; set; }
}