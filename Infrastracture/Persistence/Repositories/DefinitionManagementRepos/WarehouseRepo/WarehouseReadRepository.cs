using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.WarehouseRepo
{
    public class WarehouseReadRepository : ReadRepository<Warehouse>, IWarehouseReadRepository
    {
        private readonly Emasist2024Context _context;
        public WarehouseReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
