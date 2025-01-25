using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;

namespace Persistence.Repositories.CommunicationManagements.StudentAnnouncementRepo
{
    public class StudentAnnouncementReadRepository : ReadRepository<StudentAnnouncement>, IStudentAnnouncementReadRepository
    {
        private readonly KampusCUContext _context;
        public StudentAnnouncementReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
