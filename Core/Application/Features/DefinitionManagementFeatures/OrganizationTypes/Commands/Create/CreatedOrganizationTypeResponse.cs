using Application.Features.Base;
using Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetByGid;
using Core.Application.Responses;
using System.Configuration;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Commands.Create;

public class CreatedOrganizationTypeResponse : BaseResponse, IResponse
{
    public GetByGidOrganizationTypeResponse Obj { get; set; }
}