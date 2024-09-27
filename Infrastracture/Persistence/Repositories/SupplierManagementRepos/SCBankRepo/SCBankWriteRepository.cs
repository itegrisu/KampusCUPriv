using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCBankRepo
{
    public class SCBankWriteRepository : WriteRepository<SCBank>, ISCBankWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCBankWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
