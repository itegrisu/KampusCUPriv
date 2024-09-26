using Core.Entities;
//using Domain.Entities.StoreSalesManagements;

namespace Domain.Entities.StockManagements
{
    public class StockCard : BaseEntity
    {

        //public Guid GidStockCategoryFK { get; set; }
        //public Category CategoryFK { get; set; }
        //public Guid GidBrandFK { get; set; }
        //public Brand BrandFK { get; set; }
        //public Guid GidUnitFK { get; set; }
        //public Unit UnitFK { get; set; }
        //public Guid GidPriceCurrencyFK { get; set; }
        //public Currency CurrencyFK { get; set; }
        //public EnumStockCardType CardType { get; set; }
        public string? StockCode { get; set; }
        public string StockName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int TaxRate { get; set; }
        public string? Description { get; set; }
        public ICollection<StockCardImage>? StockCardImages { get; set; }
        public ICollection<StockMovement>? StockMovements { get; set; }
        //  public ICollection<StoreSaleDetail>? StoreSaleDetails { get; set; }
        // public ICollection<StoreAdvertising>? StoreAdvertisings { get; set; }

    }
}
