using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class GuestAccommodationRoom : BaseEntity
    {
        public Guid GidGuestAccommodationFK { get; set; }
        public GuestAccommodation GuestAccommodationFK { get; set; }
        public Guid GidRoomTypeFK { get; set; }
        public RoomType RoomTypeFK { get; set; }
        public DateTime Date { get; set; }

        public ICollection<GuestAccommodationResult>? GuestAccommodationResults { get; set; }
    }
}
