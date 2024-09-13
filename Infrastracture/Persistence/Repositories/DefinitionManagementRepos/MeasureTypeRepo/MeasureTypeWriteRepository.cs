using Application.Repositories.DefinitionManagementRepos.MeasureTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.MeasureTypeRepo
{
    public class MeasureTypeWriteRepository : WriteRepository<MeasureType>, IMeasureTypeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public MeasureTypeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
