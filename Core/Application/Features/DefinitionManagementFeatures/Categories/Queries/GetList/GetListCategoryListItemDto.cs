using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionFeatures.Categories.Queries.GetList;

public class GetListCategoryListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
}