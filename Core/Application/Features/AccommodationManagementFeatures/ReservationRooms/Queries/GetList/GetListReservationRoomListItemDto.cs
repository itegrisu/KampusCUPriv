using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Queries.GetList;

public class GetListReservationRoomListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidReservationDetailFK { get; set; }
    public int ReservationDetailFKRoomCount { get; set; }
    public string ReservationDetailFKRoomTypeFKName { get; set; }
    public int RoomNo { get; set; }

    public List<RoomGuests> Guests { get; set; }

}

public class RoomGuests
{
    public string Fullname { get; set; }
    public EnumGender Gender { get; set; }
    public string Country { get; set; }
}