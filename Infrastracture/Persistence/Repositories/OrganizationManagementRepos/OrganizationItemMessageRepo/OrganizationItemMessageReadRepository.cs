using Application.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationItemMessageRepo
{
    public class OrganizationItemMessageReadRepository : ReadRepository<OrganizationItemMessage>, IOrganizationItemMessageReadRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationItemMessageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
