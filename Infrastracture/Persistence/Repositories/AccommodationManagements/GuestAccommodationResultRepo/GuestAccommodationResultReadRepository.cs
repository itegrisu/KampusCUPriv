using Application.Repositories.AccommodationManagements.GuestAccommodationResultRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.GuestAccommodationResultRepo
{
    public class GuestAccommodationResultReadRepository : ReadRepository<GuestAccommodationResult>, IGuestAccommodationResultReadRepository
    {
        private readonly Emasist2024Context _context;
        public GuestAccommodationResultReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
