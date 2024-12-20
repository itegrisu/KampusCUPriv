using Core.Entities;
using Domain.Entities.DefinitionManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.AccommodationManagements
{
    public class ReservationDetail : BaseEntity
    {
        public Guid GidReservationHotelFK { get; set; }
        public ReservationHotel ReservationHotelFK { get; set; }
        public Guid GidRoomTypeFK { get; set; }
        public RoomType RoomTypeFK { get; set; }

        public DateTime ReservationDate { get; set; }
        public int RoomCount { get; set; }
        public decimal? BuyPrice { get; set; }
        public decimal? SellPrice { get; set; }

        public ICollection<ReservationRoom>? ReservationRooms { get; set; }
        public ICollection<AccommodationDate>? AccommodationDates { get; set; }
    }
}
