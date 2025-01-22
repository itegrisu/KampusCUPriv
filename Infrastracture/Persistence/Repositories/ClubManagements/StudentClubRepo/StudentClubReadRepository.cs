using Application.Repositories.ClubManagementRepos.StudentClubRepo;
using Core.Repositories.Concretes;
using Domain.Entities.ClubManagements;
using Persistence.Context;

namespace Persistence.Repositories.ClubManagements.StudentClubRepo
{
    public class StudentClubReadRepository : ReadRepository<StudentClub>, IStudentClubReadRepository
    {
        private readonly KampusCUContext _context;
        public StudentClubReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
