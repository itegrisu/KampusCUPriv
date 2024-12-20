using Core.Application.Responses;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Queries.GetByGid
{
    public class GetByGidReservationDetailResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidReservationHotelFK { get; set; }
        public string ReservationHotelFKReservationFKTitle { get; set; }
        public Guid GidRoomTypeFK { get; set; }
        public string RoomTypeFKName { get; set; }
        public DateTime ReservationDate { get; set; }
        public int RoomCount { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal SellPrice { get; set; }
    }
}