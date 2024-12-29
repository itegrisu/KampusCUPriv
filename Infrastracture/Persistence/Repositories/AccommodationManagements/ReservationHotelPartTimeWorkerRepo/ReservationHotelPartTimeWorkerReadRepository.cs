using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo
{

    public class ReservationHotelPartTimeWorkerReadRepository : ReadRepository<ReservationHotelPartTimeWorker>, IReservationHotelPartTimeWorkerReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelPartTimeWorkerReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
