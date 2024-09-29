using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.MeasureTypes.Queries.GetList;

public class GetListMeasureTypeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }


}