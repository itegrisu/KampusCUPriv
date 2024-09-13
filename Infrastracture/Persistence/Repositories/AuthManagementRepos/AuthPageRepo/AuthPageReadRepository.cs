using Application.Repositories.AuthManagementRepos.AuthPageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthPageRepo
{
    public class AuthPageReadRepository : ReadRepository<AuthPage>, IAuthPageReadRepository
    {
        private readonly Emasist2024Context _context;
        public AuthPageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
