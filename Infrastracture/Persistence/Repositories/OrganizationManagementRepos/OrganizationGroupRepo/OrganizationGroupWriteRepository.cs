using Application.Repositories.OrganizationManagementRepos.OrganizationGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationGroupRepo
{
    public class OrganizationGroupWriteRepository : WriteRepository<OrganizationGroup>, IOrganizationGroupWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationGroupWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
