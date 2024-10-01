using Application.Repositories.OrganizationManagementRepos.OrganizationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationRepo
{
    public class OrganizationWriteRepository : WriteRepository<Organization>, IOrganizationWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
