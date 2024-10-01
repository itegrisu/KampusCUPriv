using Core.Entities;
using Domain.Entities.WarehouseManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class StockCategory : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }

        public ICollection<StockCard>? StockCards { get; set; }

    }
}
