using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCCompanyRepo
{
    public class SCCompanyWriteRepository : WriteRepository<SCCompany>, ISCCompanyWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCCompanyWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
