using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.MeasureTypeRepo
{

    public class MeasureTypeReadRepository : ReadRepository<MeasureType>, IMeasureTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public MeasureTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
