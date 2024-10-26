using Application.Repositories.GeneralManagementRepos.UserReminderRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.GeneralManagementRepos.UserReminderRepo
{
    public class UserReminderWriteRepository : WriteRepository<UserReminder>, IUserReminderWriteRepository
    {
        private readonly Emasist2024Context _context;
        public UserReminderWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
