using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.PermitTypes.Queries.GetList;

public class GetListPermitTypeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }


}