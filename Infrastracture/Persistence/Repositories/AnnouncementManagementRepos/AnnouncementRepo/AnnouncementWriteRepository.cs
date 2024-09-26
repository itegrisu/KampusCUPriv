using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AnnouncementManagements;
using Persistence.Context;

namespace Persistence.Repositories.AnnouncementManagementRepos.AnnouncementRepo
{
    public class AnnouncementWriteRepository : WriteRepository<Announcement>, IAnnouncementWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AnnouncementWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
