using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.WarehouseManagements;
using Persistence.Context;

namespace Persistence.Repositories.WarehouseManagementRepos.StockMovementRepo
{

    public class StockMovementReadRepository : ReadRepository<StockMovement>, IStockMovementReadRepository
    {
        private readonly Emasist2024Context _context;
        public StockMovementReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
