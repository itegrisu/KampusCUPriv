using Application.Repositories.DefinitionManagementRepos.ForeignLanguageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.ForeignLanguageRepo
{

    public class ForeignLanguageReadRepository : ReadRepository<ForeignLanguage>, IForeignLanguageReadRepository
    {
        private readonly Emasist2024Context _context;
        public ForeignLanguageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
