using Application.Repositories.SupplierManagementRepos.SCBankRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCBankRepo
{

    public class SCBankReadRepository : ReadRepository<SCBank>, ISCBankReadRepository
    {
        private readonly Emasist2024Context _context;
        public SCBankReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
