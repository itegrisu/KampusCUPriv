using Core.Entities;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Enums;

namespace Domain.Entities.AccommodationManagements
{
    public class ReservationHotelStaff : BaseEntity
    {
        public Guid GidHotelFK { get; set; }
        public SCCompany SCCompanyFK { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? GsmNo { get; set; }
        public EnumHotelStaffStatus HotelStaffStatus { get; set; }
        public string? Password { get; set; }
        public string? PasswordHash { get; set; }
    }
}
