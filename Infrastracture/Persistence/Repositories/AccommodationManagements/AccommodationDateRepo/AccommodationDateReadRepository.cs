using Application.Repositories.AccommodationManagements.AccommodationDateRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.AccommodationDateRepo
{
    public class AccommodationDateReadRepository : ReadRepository<AccommodationDate>, IAccommodationDateReadRepository
    {
        private readonly Emasist2024Context _context;
        public AccommodationDateReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
