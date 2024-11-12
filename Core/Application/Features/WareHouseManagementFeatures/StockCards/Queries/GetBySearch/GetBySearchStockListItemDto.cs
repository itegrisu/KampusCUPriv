using Core.Application.Dtos;

namespace Application.Features.WareHouseManagementFeatures.StockCards.Queries.GetBySearch
{
    public class GetBySearchStockListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public string StockName { get; set; }
        public string WarehouseName { get; set; }
        public int Count { get; set; }
    }
}
