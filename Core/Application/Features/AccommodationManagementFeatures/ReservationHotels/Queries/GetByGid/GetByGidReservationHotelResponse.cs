using Core.Application.Responses;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Queries.GetByGid
{
    public class GetByGidReservationHotelResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidReservationFK { get; set; }
        public string ReservationFKTitle { get; set; }
        public Guid GidHotelFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidBuyCurrencyTypeFK { get; set; }
        public string BuyCurrencyFKName { get; set; }
        public Guid GidSellCurrencyTypeFK { get; set; }
        public string SellCurrencyFKName { get; set; }
    }
}