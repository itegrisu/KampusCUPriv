using Application.Repositories.LogManagementRepos.LogFailedLoginRepo;
using Core.Repositories.Concretes;
using Domain.Entities.LogManagements;
using Persistence.Context;

public class LogFailedLoginReadRepository : ReadRepository<LogFailedLogin>, ILogFailedLoginReadRepository
{
    private readonly Emasist2024Context _context;
    public LogFailedLoginReadRepository(Emasist2024Context context) : base(context)
    {
        _context = context;
    }
}