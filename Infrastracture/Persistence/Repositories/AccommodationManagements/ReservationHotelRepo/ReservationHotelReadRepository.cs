using Application.Repositories.AccommodationManagements.ReservationHotelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelRepo
{
    public class ReservationHotelReadRepository : ReadRepository<ReservationHotel>, IReservationHotelReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
