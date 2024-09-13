using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.LogManagementRepos.LogEmailSendRepo
{

    public class LogEmailSendReadRepository : ReadRepository<LogEmailSend>, ILogEmailSendReadRepository
    {
        private readonly Emasist2024Context _context;
        public LogEmailSendReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
