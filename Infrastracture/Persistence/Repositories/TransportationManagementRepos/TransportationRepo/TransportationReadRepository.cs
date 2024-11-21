using Application.Repositories.TransportationRepos.TransportationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationRepo
{
    public class TransportationReadRepository : ReadRepository<Transportation>, ITransportationReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
