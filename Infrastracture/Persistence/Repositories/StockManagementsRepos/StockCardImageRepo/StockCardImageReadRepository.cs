using Application.Repositories.StockManagementRepos.StockCardImageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.StockManagements;
using Persistence.Context;

namespace Persistence.Repositories.StockManagementsRepos.StockCardImageRepo
{

    public class StockCardImageReadRepository : ReadRepository<StockCardImage>, IStockCardImageReadRepository
    {
        private readonly Emasist2024Context _context;
        public StockCardImageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
