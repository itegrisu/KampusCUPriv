using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionFeatures.Classes.Queries.GetList;

public class GetListClassListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
}