using Application.Repositories.FinanceManagementRepos.FinanceExpenseDetailRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceExpenceDetailRepo
{
    public class FinanceExpenseDetailWriteRepository : WriteRepository<FinanceExpenseDetail>, IFinanceExpenseDetailWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceExpenseDetailWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
