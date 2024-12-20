using Application.Repositories.AccommodationManagements.ReservationRoomRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationRoomRepo
{
    public class ReservationRoomReadRepository : ReadRepository<ReservationRoom>, IReservationRoomReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationRoomReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
