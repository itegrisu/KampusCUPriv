using Core.Application.Dtos;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetListByWorker
{
    public class GetListByWorkerReservationHotelListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidReservationFK { get; set; }
        public string ReservationFKTitle { get; set; }
        public Guid GidHotelFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidBuyCurrencyTypeFK { get; set; }
        public string BuyCurrencyFKName { get; set; }
        public string BuyCurrencyFKSymbol { get; set; }
        public Guid GidSellCurrencyTypeFK { get; set; }
        public string SellCurrencyFKName { get; set; }
        public string SellCurrencyFKSymbol { get; set; }
    }
}