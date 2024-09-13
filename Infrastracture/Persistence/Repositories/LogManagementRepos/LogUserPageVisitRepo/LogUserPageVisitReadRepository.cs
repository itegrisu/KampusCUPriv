using Application.Repositories.LogManagementRepos.LogUserPageVisitRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogUserPageVisitRepo
{

    public class LogUserPageVisitReadRepository : ReadRepository<LogUserPageVisit>, ILogUserPageVisitReadRepository
    {
        private readonly Emasist2024Context _context;
        public LogUserPageVisitReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
