using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionFeatures.Departments.Queries.GetList;

public class GetListDepartmentListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
}