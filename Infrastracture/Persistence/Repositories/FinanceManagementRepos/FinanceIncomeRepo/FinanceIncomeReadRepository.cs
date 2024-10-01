using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceIncomeRepo
{

    public class FinanceIncomeReadRepository : ReadRepository<FinanceIncome>, IFinanceIncomeReadRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceIncomeReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
