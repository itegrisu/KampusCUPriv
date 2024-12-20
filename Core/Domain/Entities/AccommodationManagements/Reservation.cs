using Core.Entities;
using Domain.Entities.OrganizationManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class Reservation : BaseEntity
    {
        public Guid? GidOrganizationFK { get; set; }
        public Organization? OrganizationFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? EstimatedGuestCount { get; set; }
        public int? EstimatedAccommodationCount { get; set; }
        public EnumReservationType ReservationType { get; set; }
        public EnumReservationStatus ReservationStatus { get; set; }

        public ICollection<ReservationHotel>? ReservationHotels { get; set; }
    }
}
