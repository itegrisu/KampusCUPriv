using Application.Repositories.AccommodationManagements.PartTimeWorkerFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.PartTimeWorkerFileRepo
{

    public class PartTimeWorkerFileReadRepository : ReadRepository<PartTimeWorkerFile>, IPartTimeWorkerFileReadRepository
    {
        private readonly Emasist2024Context _context;
        public PartTimeWorkerFileReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
