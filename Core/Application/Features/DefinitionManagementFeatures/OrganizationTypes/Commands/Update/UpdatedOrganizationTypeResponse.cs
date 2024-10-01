using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Update;

public class UpdatedOrganizationTypeResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationTypeResponse Obj { get; set; }
}