using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationGroupRepo
{
    public class TransportationGroupReadRepository : ReadRepository<TransportationGroup>, ITransportationGroupReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationGroupReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
