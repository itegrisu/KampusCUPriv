using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.OrganizationTypeRepo
{

    public class OrganizationTypeReadRepository : ReadRepository<OrganizationType>, IOrganizationTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
