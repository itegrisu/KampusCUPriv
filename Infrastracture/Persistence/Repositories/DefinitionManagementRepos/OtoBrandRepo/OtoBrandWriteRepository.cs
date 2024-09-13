using Application.Repositories.DefinitionManagementRepos.OtoBrandRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.OtoBrandRepo
{
    public class OtoBrandWriteRepository : WriteRepository<OtoBrand>, IOtoBrandWriteRepository
    {
        private readonly Emasist2024Context _context;
        public OtoBrandWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
