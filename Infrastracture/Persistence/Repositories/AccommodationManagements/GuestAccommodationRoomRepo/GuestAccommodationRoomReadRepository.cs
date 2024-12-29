using Application.Repositories.AccommodationManagements.GuestAccommodationRoomRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationRoomRepo
{
    public class GuestAccommodationRoomReadRepository : ReadRepository<GuestAccommodationRoom>, IGuestAccommodationRoomReadRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationRoomReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
