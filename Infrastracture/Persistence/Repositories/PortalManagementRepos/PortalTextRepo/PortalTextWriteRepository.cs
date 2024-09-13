using Application.Repositories.PortalManagementRepos.PortalTextRepo;
using Core.Repositories.Concretes;
using Domain.Entities.PortalManagements;
using Persistence.Context;

namespace Persistence.Repositories.PortalManagementRepos.PortalTextRepo
{
    public class PortalTextWriteRepository : WriteRepository<PortalText>, IPortalTextWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PortalTextWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
