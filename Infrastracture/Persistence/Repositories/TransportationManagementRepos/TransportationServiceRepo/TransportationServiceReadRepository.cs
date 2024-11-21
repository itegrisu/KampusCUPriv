using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationServiceRepo
{
    public class TransportationServiceReadRepository : ReadRepository<TransportationService>, ITransportationServiceReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationServiceReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
