using Core.Application.Dtos;

namespace Application.Features.WareHouseManagementFeatures.StockCards.Queries.GetByWarehouse
{
    public class GetByWarehouseStockListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidStockCategoryFK { get; set; }
        public string StockCategoryFKName { get; set; }
        public Guid GidMeasureFK { get; set; }
        public string MeasureTypeFKName { get; set; }

        public string? StockCode { get; set; }
        public string StockName { get; set; }
        public string? Description { get; set; }

        public int Count { get; set; }

    }
}