using Application.Repositories.AccommodationManagements.GuestAccommodationPersonRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationPersonRepo
{
    public class GuestAccommodationPersonReadRepository : ReadRepository<GuestAccommodationPerson>, IGuestAccommodationPersonReadRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationPersonReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
