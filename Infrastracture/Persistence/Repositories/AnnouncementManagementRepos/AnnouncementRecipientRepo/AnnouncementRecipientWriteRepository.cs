using Application.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AnnouncementManagements;
using Persistence.Context;

namespace Persistence.Repositories.AnnouncementManagementRepos.AnnouncementRecipientRepo
{
    public class AnnouncementRecipientWriteRepository : WriteRepository<AnnouncementRecipient>, IAnnouncementRecipientWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AnnouncementRecipientWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
