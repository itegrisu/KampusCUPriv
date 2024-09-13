using Application.Repositories.LogManagementRepos.LogSuccessedLoginRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogSuccessedLoginRepo
{
    public class LogSuccessedLoginReadRepository : ReadRepository<LogSuccessedLogin>, ILogSuccessedLoginReadRepository
    {
        private readonly Emasist2024Context _context;
        public LogSuccessedLoginReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
