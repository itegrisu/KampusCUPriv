using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.GeneralManagements.AdminRepo
{
    public class AdminWriteRepository : WriteRepository<Admin>, IAdminWriteRepository
    {
        private readonly KampusCUContext _context;
        public AdminWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
