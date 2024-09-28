using Application.Repositories.SupplierManagementRepos.SCWorkHistoryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCWorkHistoryRepo
{
    public class SCWorkHistoryWriteRepository : WriteRepository<SCWorkHistory>, ISCWorkHistoryWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCWorkHistoryWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
