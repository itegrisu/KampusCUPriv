using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Queries.GetList;

public class GetListStockMovementListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidStockCardFK { get; set; }
    public string StockCardFKStockName { get; set; }
    public Guid? GidPreviousWarehouseFK { get; set; }
    public string PreviousWarehouseFKName { get; set; }
    public Guid? GidNextWarehouseFK { get; set; }
    public string NextWarehouseFKName { get; set; }
    public EnumOperationType OperationType { get; set; }
    public EnumMovementType MovementType { get; set; }
    public DateTime TransactionDate { get; set; }
    public int Amount { get; set; }
    public string? Document { get; set; }
    public string? Description { get; set; }

}