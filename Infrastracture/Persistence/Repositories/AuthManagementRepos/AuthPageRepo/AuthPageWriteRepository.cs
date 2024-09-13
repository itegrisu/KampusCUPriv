using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthPageRepo
{
    public class AuthPageWriteRepository : WriteRepository<AuthPage>, IAuthPageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AuthPageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
