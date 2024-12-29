using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class GuestAccommodationResult : BaseEntity
    {
        public Guid GidGuestAccommodationPersonFK { get; set; }
        public GuestAccommodationPerson GuestAccommodationPersonFK { get; set; }
        public Guid GidGuestAccommodationRoomFK { get; set; }
        public GuestAccommodationRoom GuestAccommodationRoomFK { get; set; }
        public string? Note { get; set; }
    }
}
