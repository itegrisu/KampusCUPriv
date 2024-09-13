using Application.Repositories.DefinitionManagementRepos.CountryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.CountryRepo
{
    public class CountryWriteRepository : WriteRepository<Country>, ICountryWriteRepository
    {
        private readonly Emasist2024Context _context;
        public CountryWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
