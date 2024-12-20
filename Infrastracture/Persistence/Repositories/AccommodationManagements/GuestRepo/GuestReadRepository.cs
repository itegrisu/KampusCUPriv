using Application.Repositories.AccommodationManagements.GuestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.GuestRepo
{
    public class GuestReadRepository : ReadRepository<Guest>, IGuestReadRepository
    {
        private readonly Emasist2024Context _context;
        public GuestReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
