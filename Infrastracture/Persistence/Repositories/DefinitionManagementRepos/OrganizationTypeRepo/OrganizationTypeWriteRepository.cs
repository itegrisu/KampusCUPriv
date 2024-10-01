using Application.Repositories.DefinitionManagementRepos.OrganizationTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.OrganizationTypeRepo
{
    public class OrganizationTypeWriteRepository : WriteRepository<OrganizationType>, IOrganizationTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OrganizationTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
