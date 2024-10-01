using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceExpenceDetailRepo
{

    public class FinanceExpenseDetailReadRepository : ReadRepository<FinanceExpenseDetail>, IFinanceExpenseDetailReadRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceExpenseDetailReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
