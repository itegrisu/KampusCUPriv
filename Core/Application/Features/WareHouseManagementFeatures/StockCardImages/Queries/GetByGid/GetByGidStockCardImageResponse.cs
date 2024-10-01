using Core.Application.Responses;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Queries.GetByGid
{
    public class GetByGidStockCardImageResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidStockCardFK { get; set; }
        public string StockCardFKStockName { get; set; }
        public string StockCardFKStockCode { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public int RowNo { get; set; }

    }
}