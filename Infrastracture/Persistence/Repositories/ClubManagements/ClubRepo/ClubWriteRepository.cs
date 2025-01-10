using Application.Repositories.ClubManagementRepos.ClubRepo;
using Core.Repositories.Concretes;
using Domain.Entities.ClubManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ClubManagements.ClubRepo
{
    public class ClubWriteRepository : WriteRepository<Club>, IClubWriteRepository
    {
        private readonly KampusCUContext _context;
        public ClubWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
