using Application.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceIncomeGroupRepo
{
    public class FinanceIncomeGroupWriteRepository : WriteRepository<FinanceIncomeGroup>, IFinanceIncomeGroupWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceIncomeGroupWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
