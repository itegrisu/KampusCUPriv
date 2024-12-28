using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetByGid
{
    public class GetByGidReservationHotelStaffResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidHotelFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public string FullName { get; set; }
        public string? GsmNo { get; set; }
        public EnumHotelStaffStatus HotelStaffStatus { get; set; }
        public string? Password { get; set; }
        //public string? PasswordHash { get; set; }
    }
}