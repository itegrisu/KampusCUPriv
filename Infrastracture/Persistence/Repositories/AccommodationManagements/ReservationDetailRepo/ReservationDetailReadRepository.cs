using Application.Repositories.AccommodationManagements.ReservationDetailRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationDetailRepo
{
    public class ReservationDetailReadRepository : ReadRepository<ReservationDetail>, IReservationDetailReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationDetailReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
