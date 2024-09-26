using Application.Repositories.StockManagementRepos.StockCardRepo;
using Core.Repositories.Concretes;
using Domain.Entities.StockManagements;
using Persistence.Context;

namespace Persistence.Repositories.StockManagementsRepos.StockCardRepo
{

    public class StockCardReadRepository : ReadRepository<StockCard>, IStockCardReadRepository
    {
        private readonly Emasist2024Context _context;
        public StockCardReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
