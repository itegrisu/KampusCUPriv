using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.ForeignLanguageRepo
{
    public class ForeignLanguageWriteRepository : WriteRepository<ForeignLanguage>, IForeignLanguageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public ForeignLanguageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
