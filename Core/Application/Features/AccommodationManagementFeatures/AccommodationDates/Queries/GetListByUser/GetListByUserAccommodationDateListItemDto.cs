using Core.Application.Dtos;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Queries.GetListByUser
{
    public class GetListByUserAccommodationDateListItemDto : IDto
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

        public string? GuestFKCountryFKName { get; set; }
        public string GuestFKIdNumber { get; set; } = string.Empty;
        public string? GuestFKDuty { get; set; }
        public string? GuestFKInstitution { get; set; }
        public string? GuestFKPhone { get; set; }
        public string? GuestFKEmail { get; set; }
        public EnumGender GuestFKGender { get; set; }
        public string? GuestFKHesCode { get; set; }
        public string? GuestFKDescription { get; set; }

    }
}