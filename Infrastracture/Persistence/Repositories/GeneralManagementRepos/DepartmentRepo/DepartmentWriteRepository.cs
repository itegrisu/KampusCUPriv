using Application.Repositories.GeneralManagementRepos.DepartmentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.DepartmentRepo
{
    public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
    {
        private readonly Emasist2024Context _context;
        public DepartmentWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
