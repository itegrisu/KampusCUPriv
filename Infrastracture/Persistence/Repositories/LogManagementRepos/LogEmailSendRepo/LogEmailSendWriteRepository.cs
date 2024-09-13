using Application.Repositories.LogManagementRepos.LogEmailSendRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogEmailSendRepo
{
    public class LogEmailSendWriteRepository : WriteRepository<LogEmailSend>, ILogEmailSendWriteRepository
    {
        private readonly Emasist2024Context _context;
        public LogEmailSendWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
