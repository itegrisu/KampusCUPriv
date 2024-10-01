using Application.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceExpenseGroupRepo
{
    public class FinanceExpenseGroupWriteRepository : WriteRepository<FinanceExpenseGroup>, IFinanceExpenseGroupWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceExpenseGroupWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
