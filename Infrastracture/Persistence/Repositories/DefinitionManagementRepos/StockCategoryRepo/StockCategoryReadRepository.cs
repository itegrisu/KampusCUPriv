using Application.Repositories.DefinitionManagementRepos.StockCategoryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagementRepos.StockCategoryRepo
{

    public class StockCategoryReadRepository : ReadRepository<StockCategory>, IStockCategoryReadRepository
    {
        private readonly Emasist2024Context _context;
        public StockCategoryReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
