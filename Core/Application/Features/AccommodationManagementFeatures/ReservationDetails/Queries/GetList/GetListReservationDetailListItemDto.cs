using Core.Application.Dtos;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetList;

public class GetListReservationDetailListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidReservationHotelFK { get; set; }
    public string ReservationHotelFKSCCompanyFKCompanyName { get; set; }
    public string ReservationHotelFKReservationFKTitle { get; set; }
    public string ReservationHotelFKBuyCurrencyFKSymbol { get; set; }
    public string ReservationHotelFKSellCurrencyFKSymbol { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public string RoomTypeFKName { get; set; }
    public int RoomTypeFKCapacity { get; set; }
    public DateTime ReservationDate { get; set; }
    public int RoomCount { get; set; }
    public decimal BuyPrice { get; set; }
    public decimal SellPrice { get; set; }
}