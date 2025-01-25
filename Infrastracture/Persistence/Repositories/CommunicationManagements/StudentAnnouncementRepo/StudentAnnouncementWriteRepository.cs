using Application.Repositories.CommunicationManagementRepo.StudentAnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CommunicationManagements.StudentAnnouncementRepo
{
    public class StudentAnnouncementWriteRepository : WriteRepository<StudentAnnouncement>, IStudentAnnouncementWriteRepository
    {
        private readonly KampusCUContext _context;
        public StudentAnnouncementWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
