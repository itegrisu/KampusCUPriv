using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.GeneralManagements.UserRepo
{
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        private readonly KampusCUContext _context;
        public UserWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
