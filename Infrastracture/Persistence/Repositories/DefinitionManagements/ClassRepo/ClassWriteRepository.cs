using Application.Repositories.DefinitionManagementRepo.ClassRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagements.ClassRepo
{
    public class ClassWriteRepository : WriteRepository<Class>, IClassWriteRepository
    {
        private readonly KampusCUContext _context;
        public ClassWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
