using Application.Repositories.DefinitionManagementRepos.CityRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.CityRepo
{

    public class CityReadRepository : ReadRepository<City>, ICityReadRepository
    {
        private readonly Emasist2024Context _context;
        public CityReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
