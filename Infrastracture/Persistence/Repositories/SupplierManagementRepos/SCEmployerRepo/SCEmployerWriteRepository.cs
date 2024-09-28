using Application.Repositories.SupplierManagementRepos.SCEmployerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.SupplierCustomerManagements;
using Persistence.Context;

namespace Persistence.Repositories.SupplierManagementRepos.SCEmployerRepo
{
    public class SCEmployerWriteRepository : WriteRepository<SCEmployer>, ISCEmployerWriteRepository
    {
        private readonly Emasist2024Context _context;
        public SCEmployerWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
