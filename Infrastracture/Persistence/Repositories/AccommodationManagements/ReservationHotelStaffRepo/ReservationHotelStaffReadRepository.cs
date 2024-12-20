using Application.Repositories.AccommodationManagements.ReservationHotelStaffRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.ReservationHotelStaffRepo
{
    public class ReservationHotelStaffReadRepository : ReadRepository<ReservationHotelStaff>, IReservationHotelStaffReadRepository
    {
        private readonly Emasist2024Context _context;
        public ReservationHotelStaffReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
