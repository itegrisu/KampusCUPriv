using Application.Repositories.DefinitonManagementRepos.WarehouseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.WarehouseRepo
{
    public class WarehouseWriteRepository : WriteRepository<Warehouse>, IWarehouseWriteRepository
    {
        private readonly Emasist2024Context _context;
        public WarehouseWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
