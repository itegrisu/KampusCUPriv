using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogAuthorizationErrorRepo
{
    public class LogAuthorizationErrorWriteRepository : WriteRepository<LogAuthorizationError>, ILogAuthorizationErrorWriteRepository
    {
        private readonly Emasist2024Context _context;
        public LogAuthorizationErrorWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}