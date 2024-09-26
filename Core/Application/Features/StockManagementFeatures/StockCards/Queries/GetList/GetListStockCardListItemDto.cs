using Core.Application.Dtos;

namespace Application.Features.StockManagementFeatures.StockCards.Queries.GetList;

public class GetListStockCardListItemDto : IDto
{
    public Guid Gid { get; set; }
    //public Guid GidStockCategoryFK { get; set; }
    //public string CategoryFKCategoryName { get; set; }
    //public Guid GidBrandFK { get; set; }
    //public string BrandFKBrandName { get; set; }
    //public Guid GidUnitFK { get; set; }
    //public string UnitFKUnitName { get; set; }
    //public Guid GidPriceCurrencyFK { get; set; }
    //public string CurrencyFKCurrencyName { get; set; }
    //public EnumCardType CardType { get; set; }
    public string? StockCode { get; set; }
    public string StockName { get; set; }
    public decimal Price { get; set; }
    public int TaxRate { get; set; }
    public string? Description { get; set; }


}