using Application.Repositories.StockManagementRepos.StockCardRepo;
using Core.Repositories.Concretes;
using Domain.Entities.StockManagements;
using Persistence.Context;

namespace Persistence.Repositories.StockManagementsRepos.StockCardRepo
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
