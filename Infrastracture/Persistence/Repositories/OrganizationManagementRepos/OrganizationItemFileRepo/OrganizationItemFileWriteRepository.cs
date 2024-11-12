using Application.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationItemFileRepo
{
    public class OrganizationItemFileWriteRepository : WriteRepository<OrganizationItemFile>, IOrganizationItemFileWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationItemFileWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
