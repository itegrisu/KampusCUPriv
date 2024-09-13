using Application.Repositories.DefinitionManagementRepos.CurrencyRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.CurrencyRepo
{
    public class CurrencyWriteRepository : WriteRepository<Currency>, ICurrencyWriteRepository
    {
        private readonly Emasist2024Context _context;
        public CurrencyWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
