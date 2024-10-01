using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WarehouseManagements
{
    public class StockMovement : BaseEntity
    {
        public Guid GidStockCardFK { get; set; }
        public StockCard StockCardFK { get; set; }
        public Guid? GidPreviousWarehouseFK { get; set; }
        public Warehouse? PreviousWarehouseFK { get; set; }
        public Guid? GidNextWarehouseFK { get; set; }
        public Warehouse? NextWarehouseFK { get; set; }
        public EnumOperationType OperationType { get; set; }
        public EnumMovementType MovementType { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Amount { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }


    }
}
