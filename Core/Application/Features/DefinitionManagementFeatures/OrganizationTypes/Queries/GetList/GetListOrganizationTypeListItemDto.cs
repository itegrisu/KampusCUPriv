using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionManagementFeatures.OrganizationTypes.Queries.GetList;

public class GetListOrganizationTypeListItemDto : IDto
{
    public Guid Gid { get; set; }

public string Name { get; set; }


}