using Application.Repositories.AnnouncementManagementRepos.AnnouncementRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AnnouncementManagements;
using Persistence.Context;

namespace Persistence.Repositories.AnnouncementManagementRepos.AnnouncementRepo
{
    public class AnnouncementReadRepository : ReadRepository<Announcement>, IAnnouncementReadRepository
    {
        private readonly Emasist2024Context _context;
        public AnnouncementReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
