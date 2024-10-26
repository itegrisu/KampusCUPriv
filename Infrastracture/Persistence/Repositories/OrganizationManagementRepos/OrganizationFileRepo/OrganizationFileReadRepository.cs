using Application.Repositories.OrganizationManagementRepos.OrganizationFileRepo;
using Core.Repositories.Concretes;
using Domain.Entities.OrganizationManagements;
using Persistence.Context;

namespace Persistence.Repositories.OrganizationManagementRepos.OrganizationFileRepo
{
    public class OrganizationFileReadRepository : ReadRepository<OrganizationFile>, IOrganizationFileReadRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationFileReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
