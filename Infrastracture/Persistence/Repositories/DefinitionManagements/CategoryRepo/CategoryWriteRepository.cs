using Application.Repositories.DefinitionManagementRepo.CategoryRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagements.CategoryRepo
{
    public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
    {
        private readonly KampusCUContext _context;
        public CategoryWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
