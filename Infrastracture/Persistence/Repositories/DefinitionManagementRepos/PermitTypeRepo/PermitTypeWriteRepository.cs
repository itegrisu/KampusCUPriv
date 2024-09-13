using Application.Repositories.DefinitionManagementRepos.PermitTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.PermitTypeRepo
{
    public class PermitTypeWriteRepository : WriteRepository<PermitType>, IPermitTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public PermitTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
