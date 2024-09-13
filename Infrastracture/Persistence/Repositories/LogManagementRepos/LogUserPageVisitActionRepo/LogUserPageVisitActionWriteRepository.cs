using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogUserPageVisitActionRepo
{
    public class LogUserPageVisitActionWriteRepository : WriteRepository<LogUserPageVisitAction>, ILogUserPageVisitActionWriteRepository
    {
        private readonly Emasist2024Context _context;
        public LogUserPageVisitActionWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
