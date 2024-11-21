using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationPassengerRepo
{
    public class TransportationPassengerReadRepository : ReadRepository<TransportationPassenger>, ITransportationPassengerReadRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationPassengerReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
