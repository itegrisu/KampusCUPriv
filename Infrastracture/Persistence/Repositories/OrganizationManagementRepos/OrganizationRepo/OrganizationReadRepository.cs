using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationRepo
{

    public class OrganizationReadRepository : ReadRepository<Organization>, IOrganizationReadRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
