using Application.Repositories.GeneralManagementRepos.DepartmentUserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.DepartmentUserRepo
{

    public class DepartmentUserReadRepository : ReadRepository<DepartmentUser>, IDepartmentUserReadRepository
    {
        private readonly Emasist2024Context _context;
        public DepartmentUserReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
