using Application.Repositories.AuthManagementRepos.AuthUserRoleRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthUserRoleRepo
{
    public class AuthUserRoleWriteRepository : WriteRepository<AuthUserRole>, IAuthUserRoleWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AuthUserRoleWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
