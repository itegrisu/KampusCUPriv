using Core.Application.Dtos;
using System.Text.Json.Serialization;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetList;

public class GetListAccommodationDateListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidReservationDetailFK { get; set; }
    public int ReservationDetailFKRoomCount { get; set; }

    [JsonPropertyName("hotelName")]
    public string ReservationDetailFKReservationHotelFKSCCompanyFKCompanyName { get; set; }

    public Guid? GidGuestFK { get; set; }
    public string GuestFKName { get; set; }
    public string GuestFKSurename { get; set; }
    public Guid? GidRoomNoFK { get; set; }
    public int ReservationRoomFKRoomNo { get; set; }
    [JsonPropertyName("roomName")]
    public string ReservationDetailFKRoomTypeFKName { get; set; }
    public DateTime Date { get; set; }
    public string? PreviousRoomInfo { get; set; }

}