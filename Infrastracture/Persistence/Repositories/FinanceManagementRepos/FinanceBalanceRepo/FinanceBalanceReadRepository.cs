using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceBalanceRepo
{
    public class FinanceBalanceReadRepository : ReadRepository<FinanceBalance>, IFinanceBalanceReadRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceBalanceReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
