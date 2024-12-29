using Application.Repositories.AccommodationManagements.PartTimeWorkerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AccommodationManagements;
using Persistence.Context;

namespace Persistence.Repositories.AccommodationManagements.PartTimeWorkerRepo
{

    public class PartTimeWorkerReadRepository : ReadRepository<PartTimeWorker>, IPartTimeWorkerReadRepository
    {
        private readonly Emasist2024Context _context;
        public PartTimeWorkerReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
