using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetByGid
{
    public class GetByGidGuestAccommodationRoomResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidGuestAccommodationFK { get; set; }
        public string GuestAccommodationFKTitle { get; set; }
        public Guid GidRoomTypeFK { get; set; }
        public string RoomTypeFKName { get; set; }
        public DateTime Date { get; set; }
    }
}