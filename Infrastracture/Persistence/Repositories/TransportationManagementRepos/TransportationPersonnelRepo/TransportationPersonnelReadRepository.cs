using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationPersonnelRepo
{
    public class TransportationPersonnelReadRepository : ReadRepository<TransportationPersonnel>, ITransportationPersonnelReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationPersonnelReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
