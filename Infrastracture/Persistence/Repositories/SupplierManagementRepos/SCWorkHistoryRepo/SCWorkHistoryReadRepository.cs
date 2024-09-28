using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCWorkHistoryRepo
{

    public class SCWorkHistoryReadRepository : ReadRepository<SCWorkHistory>, ISCWorkHistoryReadRepository
    {
        private readonly Emasist2024Context _context;
        public SCWorkHistoryReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
