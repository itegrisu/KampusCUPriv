using Core.Entities;

namespace Domain.Entities.AccommodationManagements
{
    public class ReservationHotelPartTimeWorker : BaseEntity
    {

        public Guid GidHotelFK { get; set; }
        public ReservationHotel ReservationHotelFK { get; set; }
        public Guid GidPartTimeWorkerFK { get; set; }
        public PartTimeWorker PartTimeWorkerFK { get; set; }

        public bool IsActive { get; set; }
        public string? Note { get; set; }


    }
}
