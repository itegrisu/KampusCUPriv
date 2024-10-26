using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserReminderRepo
{
    public class UserReminderReadRepository : ReadRepository<UserReminder>, IUserReminderReadRepository
    {
        private readonly Emasist2024Context _context;
        public UserReminderReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
