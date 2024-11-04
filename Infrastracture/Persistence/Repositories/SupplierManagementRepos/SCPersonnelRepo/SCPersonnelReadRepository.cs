using Application.Repositories.SupplierManagementRepos.SCPersonnelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCPersonnelRepo
{
    public class SCPersonnelReadRepository : ReadRepository<SCPersonnel>, ISCPersonnelReadRepository
    {
        private readonly Emasist2024Context _context;
        public SCPersonnelReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
