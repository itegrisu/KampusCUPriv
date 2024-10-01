using Application.Repositories.FinanceManagementRepos.FinanceIncomeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceIncomeRepo
{
    public class FinanceIncomeWriteRepository : WriteRepository<FinanceIncome>, IFinanceIncomeWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceIncomeWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
