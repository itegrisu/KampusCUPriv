using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Queries.GetList;

public class GetListStockCategoryListItemDto : IDto
{
    public Guid Gid { get; set; }

public string Name { get; set; }
public string? Code { get; set; }


}