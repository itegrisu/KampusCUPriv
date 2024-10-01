using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Queries.GetList;

public class GetListStockCardListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidStockCategoryFK { get; set; }
    public string StockCategoryFKName { get; set; }
    public Guid GidMeasureFK { get; set; }
    public string MeasureTypeFKName { get; set; }
    public EnumStockType StockType { get; set; }
    public string StockName { get; set; }
    public string? StockCode { get; set; }
    public string? Brand { get; set; }
    public string? Description { get; set; }


}