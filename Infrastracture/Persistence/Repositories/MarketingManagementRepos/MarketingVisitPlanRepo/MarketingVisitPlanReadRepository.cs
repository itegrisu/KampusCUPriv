using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using Core.Repositories.Concretes;
using Domain.Entities.MarketingManagements;
using Persistence.Context;

namespace Persistence.Repositories.MarketingManagementRepos.MarketingVisitPlanRepo
{

    public class MarketingVisitPlanReadRepository : ReadRepository<MarketingVisitPlan>, IMarketingVisitPlanReadRepository
    {
        private readonly Emasist2024Context _context;
        public MarketingVisitPlanReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
