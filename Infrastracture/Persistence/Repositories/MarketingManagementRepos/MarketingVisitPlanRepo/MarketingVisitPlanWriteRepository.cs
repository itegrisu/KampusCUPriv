using Application.Repositories.MarketingManagementsRepos.MerketingVisitPlanRepo;
using Core.Repositories.Concretes;
using Domain.Entities.MarketingManagements;
using Persistence.Context;

namespace Persistence.Repositories.MarketingManagementRepos.MarketingVisitPlanRepo
{
    public class MarketingVisitPlanWriteRepository : WriteRepository<MarketingVisitPlan>, IMarketingVisitPlanWriteRepository
    {
        private readonly Emasist2024Context _context;
        public MarketingVisitPlanWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
