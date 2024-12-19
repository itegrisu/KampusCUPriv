using Application.Repositories.NotificationManagementRepos;
using Core.Repositories.Concretes;
using Domain.Entities.NotificationManagements;
using Persistence.Context;

namespace Persistence.Repositories.NotificationManagementRepos
{
    public class NotificationReadRepository : ReadRepository<Notification>, INotificationReadRepository
    {
        private readonly Emasist2024Context _context;
        public NotificationReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
