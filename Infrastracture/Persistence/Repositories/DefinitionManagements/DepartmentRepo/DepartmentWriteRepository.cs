using Application.Repositories.DefinitionManagementRepo.DepartmentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagements.DepartmentRepo
{
    public class DepartmentWriteRepository : WriteRepository<Department>, IDepartmentWriteRepository
    {
        private readonly KampusCUContext _context;
        public DepartmentWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
