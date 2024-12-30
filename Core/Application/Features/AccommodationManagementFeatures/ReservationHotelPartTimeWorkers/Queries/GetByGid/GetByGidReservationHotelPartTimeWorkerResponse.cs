using Core.Application.Responses;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Queries.GetByGid
{
    public class GetByGidReservationHotelPartTimeWorkerResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidHotelFK { get; set; }
        //public ReservationHotel ReservationHotelFK { get; set; }
        public Guid GidPartTimeWorkerFK { get; set; }
        public string PartTimeWorkerFKGsm { get; set; }
        public string PartTimeWorkerFKFullName { get; set; }

        public bool IsActive { get; set; }
        public string? Note { get; set; }

    }
}