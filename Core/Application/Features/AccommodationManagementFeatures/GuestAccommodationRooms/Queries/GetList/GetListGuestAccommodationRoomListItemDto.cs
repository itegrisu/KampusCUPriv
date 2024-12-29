using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Queries.GetList;

public class GetListGuestAccommodationRoomListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidGuestAccommodationFK { get; set; }
    public string GuestAccommodationFKTitle { get; set; }
    public Guid GidRoomTypeFK { get; set; }
    public string RoomTypeFKName { get; set; }
    public DateTime Date { get; set; }
}