using Application.Repositories.SupplierManagementRepos.SCCompanyRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCCompanyRepo
{

    public class SCCompanyReadRepository : ReadRepository<SCCompany>, ISCCompanyReadRepository
    {
        private readonly Emasist2024Context _context;
        public SCCompanyReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
