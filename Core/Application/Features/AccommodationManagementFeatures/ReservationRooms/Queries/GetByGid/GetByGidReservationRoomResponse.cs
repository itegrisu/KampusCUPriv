using Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;
using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid
{
    public class GetByGidReservationRoomResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidReservationDetailFK { get; set; }
        public int ReservationDetailFKRoomCount { get; set; }
        public string ReservationDetailFKRoomTypeFKName { get; set; }
        public int RoomNo { get; set; }
        public List<RoomGuests> Guests { get; set; }
    }
}