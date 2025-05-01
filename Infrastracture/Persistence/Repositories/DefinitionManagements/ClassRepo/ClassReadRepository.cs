using Application.Repositories.DefinitionManagementRepo.ClassRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagements.ClassRepo
{
    public class ClassReadRepository : ReadRepository<Class>, IClassReadRepository
    {
        private readonly KampusCUContext _context;
        public ClassReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
