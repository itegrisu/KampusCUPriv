using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo
{

    public class FinanceIncomeGroupReadRepository : ReadRepository<FinanceIncomeGroup>, IFinanceIncomeGroupReadRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceIncomeGroupReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
