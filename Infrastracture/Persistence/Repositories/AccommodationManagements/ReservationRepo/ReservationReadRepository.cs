using Application.Repositories.AccommodationManagements.ReservationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationRepo
{
    public class ReservationReadRepository : ReadRepository<Reservation>, IReservationReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
