using Application.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelPartTimeWorkerRepo
{
    public class ReservationHotelPartTimeWorkerWriteRepository : WriteRepository<ReservationHotelPartTimeWorker>, IReservationHotelPartTimeWorkerWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelPartTimeWorkerWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
