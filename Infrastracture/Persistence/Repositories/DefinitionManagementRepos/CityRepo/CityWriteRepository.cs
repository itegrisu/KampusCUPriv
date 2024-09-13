using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.CityRepo
{
    public class CityWriteRepository : WriteRepository<City>, ICityWriteRepository
    {
        private readonly Emasist2024Context _context;
        public CityWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
