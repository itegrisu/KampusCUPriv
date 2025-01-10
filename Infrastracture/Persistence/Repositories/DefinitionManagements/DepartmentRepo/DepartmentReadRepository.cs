using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagements.DepartmentRepo
{
    public class DepartmentReadRepository : ReadRepository<Department>, IDepartmentReadRepository
    {
        private readonly KampusCUContext _context;
        public DepartmentReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
