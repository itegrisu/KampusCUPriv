using Application.Repositories.StockManagementRepos.StockMovementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.StockManagements;
using Persistence.Context;

namespace Persistence.Repositories.StockManagementsRepos.StockMovementRepo
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
