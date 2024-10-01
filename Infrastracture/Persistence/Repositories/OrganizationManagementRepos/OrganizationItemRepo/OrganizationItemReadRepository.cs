using Application.Repositories.OrganizationManagementRepos.OrganizationItemRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationItemRepo
{

    public class OrganizationItemReadRepository : ReadRepository<OrganizationItem>, IOrganizationItemReadRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationItemReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
