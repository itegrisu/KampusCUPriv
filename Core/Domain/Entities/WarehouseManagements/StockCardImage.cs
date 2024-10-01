using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WarehouseManagements
{
    public class StockCardImage : BaseEntity, IHasRowNo
    {
        public Guid GidStockCardFK { get; set; }
        public StockCard StockCardFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int RowNo { get; set; }

    }
}
