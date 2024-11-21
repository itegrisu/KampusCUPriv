using Application.Repositories.FinanceManagementRepos.FinanceBalanceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.FinanceManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.FinanceManagementRepos.FinanceBalanceRepo
{
    public class FinanceBalanceWriteRepository : WriteRepository<FinanceBalance>, IFinanceBalanceWriteRepository
    {
        private readonly Emasist2024Context _context;
        public FinanceBalanceWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
