using Application.Repositories.LogManagementRepos.LogAuthorizationErrorRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

public class LogAuthorizationErrorReadRepository : ReadRepository<LogAuthorizationError>, ILogAuthorizationErrorReadRepository
{
    private readonly Emasist2024Context _context;
    public LogAuthorizationErrorReadRepository(Emasist2024Context context) : base(context)
    {
        _context = context;
    }
}
