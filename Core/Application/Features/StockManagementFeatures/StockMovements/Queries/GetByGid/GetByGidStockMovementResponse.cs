using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.StockManagementFeatures.StockMovements.Queries.GetByGid
{
    public class GetByGidStockMovementResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidStockCardFK { get; set; }
        public string StockCardFKStockName { get; set; }
        public Guid? GidPreviousWarehouseFK { get; set; }
        public string PreviousWarehouseFKWarehouseName { get; set; }
        public Guid? GidNextWarehouseFK { get; set; }
        public string NextWarehouseFKWarehouseName { get; set; }
        public EnumOperationType OperationType { get; set; }
        public EnumMovementType MovementType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }

    }
}