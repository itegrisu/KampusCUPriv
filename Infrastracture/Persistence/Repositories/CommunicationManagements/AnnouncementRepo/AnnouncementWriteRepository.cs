using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CommunicationManagements.AnnouncementRepo
{
    public class AnnouncementWriteRepository : WriteRepository<Announcement>, IAnnouncementWriteRepository
    {
        private readonly KampusCUContext _context;
        public AnnouncementWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
