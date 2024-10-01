using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceExpenseRepo
{

    public class FinanceExpenseReadRepository : ReadRepository<FinanceExpense>, IFinanceExpenseReadRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceExpenseReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
