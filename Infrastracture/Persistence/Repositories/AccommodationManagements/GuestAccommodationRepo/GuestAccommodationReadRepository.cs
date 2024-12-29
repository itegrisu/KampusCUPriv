using Application.Repositories.AccommodationManagements.GuestAccommodationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationRepo
{
    public class GuestAccommodationReadRepository : ReadRepository<GuestAccommodation>, IGuestAccommodationReadRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
