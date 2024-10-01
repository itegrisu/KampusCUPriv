using Application.Repositories.WarehouseManagementRepos.StockCardRepo;
using Core.Repositories.Concretes;
using Domain.Entities.WarehouseManagements;
using Persistence.Context;

namespace Persistence.Repositories.WarehouseManagementRepos.StockCardRepo
{
    public class StockCardWriteRepository : WriteRepository<StockCard>, IStockCardWriteRepository
    {
        private readonly Emasist2024Context _context;
        public StockCardWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
