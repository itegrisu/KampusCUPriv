using Application.Repositories.CommunicationManagementRepo.AnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;

namespace Persistence.Repositories.CommunicationManagements.AnnouncementRepo
{
    public class AnnouncementReadRepository : ReadRepository<Announcement>, IAnnouncementReadRepository
    {
        private readonly KampusCUContext _context;
        public AnnouncementReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
