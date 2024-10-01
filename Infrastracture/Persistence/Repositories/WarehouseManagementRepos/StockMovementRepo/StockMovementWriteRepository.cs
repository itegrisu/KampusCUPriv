using Application.Repositories.WarehouseManagementRepos.StockMovementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.WarehouseManagements;
using Persistence.Context;

namespace Persistence.Repositories.WarehouseManagementRepos.StockMovementRepo
{
    public class StockMovementWriteRepository : WriteRepository<StockMovement>, IStockMovementWriteRepository
    {
        private readonly Emasist2024Context _context;
        public StockMovementWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
