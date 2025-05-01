using Application.Repositories.DefinitionManagementRepo.CategoryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagements.CategoryRepo
{
    public class CategoryReadRepository : ReadRepository<Category>, ICategoryReadRepository
    {
        private readonly KampusCUContext _context;
        public CategoryReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
