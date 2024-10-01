using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;

//using Domain.Entities.StoreSalesManagements;

namespace Domain.Entities.WarehouseManagements
{
    public class StockCard : BaseEntity
    {

        public Guid GidStockCategoryFK { get; set; }
        public StockCategory StockCategoryFK { get; set; }
        public Guid GidMeasureFK { get; set; }
        public MeasureType MeasureTypeFK { get; set; }
        public EnumStockType StockType { get; set; }
        public string StockName { get; set; } = string.Empty;
        public string? StockCode { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }

        public ICollection<StockCardImage>? StockCardImages { get; set; }
        public ICollection<StockMovement>? StockMovements { get; set; }
        //  public ICollection<StoreSaleDetail>? StoreSaleDetails { get; set; }
        // public ICollection<StoreAdvertising>? StoreAdvertisings { get; set; }

    }
}
