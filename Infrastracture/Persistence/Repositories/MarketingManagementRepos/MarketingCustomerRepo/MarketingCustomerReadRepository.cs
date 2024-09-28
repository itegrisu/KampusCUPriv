using Application.Repositories.MarketingManagementsRepos.MarketingCustomerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.MarketingManagements;
using Persistence.Context;

namespace Persistence.Repositories.MarketingManagementRepos.MarketingCustomerRepo
{

    public class MarketingCustomerReadRepository : ReadRepository<MarketingCustomer>, IMarketingCustomerReadRepository
    {
        private readonly Emasist2024Context _context;
        public MarketingCustomerReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
