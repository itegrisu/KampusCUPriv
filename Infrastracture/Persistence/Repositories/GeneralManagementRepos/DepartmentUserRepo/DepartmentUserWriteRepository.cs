using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.DepartmentUserRepo
{
    public class DepartmentUserWriteRepository : WriteRepository<DepartmentUser>, IDepartmentUserWriteRepository
    {
        private readonly Emasist2024Context _context;
        public DepartmentUserWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
