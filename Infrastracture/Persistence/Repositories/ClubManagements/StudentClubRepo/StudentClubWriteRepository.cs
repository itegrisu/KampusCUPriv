using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Core.Repositories.Concretes;
using Domain.Entities.ClubManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.ClubManagements.StudentClubRepo
{
    public class StudentClubWriteRepository : WriteRepository<StudentClub>, IStudentClubWriteRepository
    {
        private readonly KampusCUContext _context;
        public StudentClubWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
