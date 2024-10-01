using Application.Repositories.WarehouseManagementRepos.StockCardImageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.WarehouseManagements;
using Persistence.Context;

namespace Persistence.Repositories.WarehouseManagementRepos.StockCardImageRepo
{
    public class StockCardImageWriteRepository : WriteRepository<StockCardImage>, IStockCardImageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public StockCardImageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
