using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

namespace Persistence.Repositories.LogManagementRepos.LogFailedLoginRepo
{
    public class LogFailedLoginWriteRepository : WriteRepository<LogFailedLogin>, ILogFailedLoginWriteRepository
    {
        private readonly Emasist2024Context _context;
        public LogFailedLoginWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}