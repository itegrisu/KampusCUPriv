using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;

public class GetListAccommodationDateListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidReservationDetailFK { get; set; }
    public int ReservationDetailFKRoomCount { get; set; }
    public Guid? GidGuestFK { get; set; }
    public string GuestFKName { get; set; }
    public Guid? GidRoomNoFK { get; set; }
    public int ReservationRoomFKRoomNo { get; set; }
    public DateTime Date { get; set; }
    public string? PreviousRoomInfo { get; set; }

}