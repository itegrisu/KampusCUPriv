using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;

public class GetListReservationRoomListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidReservationDetailFK { get; set; }
    public int ReservationDetailFKRoomCount { get; set; }
    public int RoomNo { get; set; }
}