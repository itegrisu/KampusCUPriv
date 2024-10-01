using Core.Entities;
using Domain.Entities.WarehouseManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class MeasureType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<StockCard>? StockCards { get; set; }

    }
}
