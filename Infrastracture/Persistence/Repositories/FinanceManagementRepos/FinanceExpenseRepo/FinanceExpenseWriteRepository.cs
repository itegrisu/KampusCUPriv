using Application.Repositories.FinanceManagementRepos.FinanceExpenseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceExpenseRepo
{
    public class FinanceExpenseWriteRepository : WriteRepository<FinanceExpense>, IFinanceExpenseWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceExpenseWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
