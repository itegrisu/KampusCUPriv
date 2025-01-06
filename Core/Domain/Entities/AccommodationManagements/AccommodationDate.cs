using Core.Entities;

namespace Domain.Entities.AccommodationManagements
{
    public class AccommodationDate : BaseEntity
    {
        public Guid GidReservationDetailFK { get; set; }
        public ReservationDetail ReservationDetailFK { get; set; }
        public Guid? GidGuestFK { get; set; }
        public Guest? GuestFK { get; set; }
        public Guid? GidRoomNoFK { get; set; }
        public ReservationRoom? ReservationRoomFK { get; set; }
        public DateTime Date { get; set; }
        public string? PreviousRoomInfo { get; set; }
    }
}
