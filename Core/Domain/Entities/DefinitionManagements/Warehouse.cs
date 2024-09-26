using Core.Entities;
using Domain.Entities.StockManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class Warehouse : BaseEntity
    {
        public string WarehouseName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;

        public ICollection<StockMovement>? PreviousStockMovements { get; set; }
        public ICollection<StockMovement>? NextStockMovements { get; set; }

    }
}
