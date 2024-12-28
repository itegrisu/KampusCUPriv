using Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Queries.GetList;

public class GetListReservationHotelStaffListItemDto : IDto
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