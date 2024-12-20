using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class ReservationRoom : BaseEntity
    {
        public Guid GidReservationDetailFK { get; set; }
        public ReservationDetail ReservationDetailFK { get; set; }
        public int RoomNo { get; set; }

        public ICollection<AccommodationDate>? AccommodationDates { get; set; }
    }
}
