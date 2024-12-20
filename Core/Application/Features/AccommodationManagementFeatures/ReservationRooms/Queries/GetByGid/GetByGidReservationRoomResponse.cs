using Core.Application.Responses;
using Domain.Entities.AccommodationManagements;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetByGid
{
    public class GetByGidReservationRoomResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidReservationDetailFK { get; set; }
        public int ReservationDetailFKRoomCount { get; set; }
        public int RoomNo { get; set; }
    }
}