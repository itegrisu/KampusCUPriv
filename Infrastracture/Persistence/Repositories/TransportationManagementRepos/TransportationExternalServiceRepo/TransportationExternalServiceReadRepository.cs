using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationExternalServiceRepo
{
    public class TransportationExternalServiceReadRepository : ReadRepository<TransportationExternalService>, ITransportationExternalServiceReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationExternalServiceReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
