using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.StockManagementFeatures.StockCardImages.Queries.GetList;

public class GetListStockCardImageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidStockCardFK { get; set; }
    public string StockCardFKStockName { get; set; }
    public string StockCardFKStockCode { get; set; }
    public string Title { get; set; }
    public string? Image { get; set; }
    public int RowNo { get; set; }

}