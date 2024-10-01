using Application.Repositories.WarehouseManagementRepos.WarehouseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.WarehouseManagements;
using Persistence.Context;

namespace Persistence.Repositories.WarehouseManagementRepos.WarehouseRepo
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
