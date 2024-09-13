using Application.Repositories.LogManagementRepos.LogUserPageVisitActionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogUserPageVisitActionRepo
{

    public class LogUserPageVisitActionReadRepository : ReadRepository<LogUserPageVisitAction>, ILogUserPageVisitActionReadRepository
    {
        private readonly Emasist2024Context _context;
        public LogUserPageVisitActionReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
