using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Queries.GetList;

public class GetListGuestAccommodationListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidHotelFK { get; set; }
    public string SCCompanyFKCompanyName { get; set; }
    public Guid? GidBuyCurrencyFK { get; set; }
    public string BuyCurrencyFKName { get; set; }
    public Guid? GidSellCurrencyFK { get; set; }
    public string SellCurrencyFKName { get; set; }
    public string Title { get; set; }
    public string? Institution { get; set; }
    public int GuestCount { get; set; }
    public string? Description { get; set; }
    public EnumGuestAccommodationStatus GuestAccommodationStatus { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
}