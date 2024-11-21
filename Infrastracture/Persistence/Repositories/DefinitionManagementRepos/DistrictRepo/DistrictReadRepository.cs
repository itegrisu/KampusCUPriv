using Application.Repositories.DefinitionManagementRepos.DistrictRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.DistrictRepo
{
    public class DistrictReadRepository : ReadRepository<District>, IDistrictReadRepository
    {
        private readonly Emasist2024Context _context;
        public DistrictReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
