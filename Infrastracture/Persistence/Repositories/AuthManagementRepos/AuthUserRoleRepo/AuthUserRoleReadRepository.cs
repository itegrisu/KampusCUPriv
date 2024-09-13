using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthUserRoleRepo
{

    public class AuthUserRoleReadRepository : ReadRepository<AuthUserRole>, IAuthUserRoleReadRepository
    {
        private readonly Emasist2024Context _context;
        public AuthUserRoleReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
