using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogUserPageVisitRepo
{
    public class LogUserPageVisitWriteRepository : WriteRepository<LogUserPageVisit>, ILogUserPageVisitWriteRepository
    {
        private readonly Emasist2024Context _context;
        public LogUserPageVisitWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
