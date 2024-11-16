using Application.Repositories.DefinitionManagementRepos.TyreTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.TyreTypeRepo
{
    public class TyreTypeReadRepository : ReadRepository<TyreType>, ITyreTypeReadRepository
    {
        private readonly Emasist2024Context _context;
        public TyreTypeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
