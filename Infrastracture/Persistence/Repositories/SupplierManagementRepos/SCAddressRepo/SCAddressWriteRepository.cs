using Application.Repositories.SupplierManagementRepos.SCAddressRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCAddressRepo
{
    public class SCAddressWriteRepository : WriteRepository<SCAddress>, ISCAddressWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCAddressWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
