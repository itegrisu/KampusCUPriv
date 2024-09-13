using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthRolePageRepo
{
    public class AuthRolePageReadRepository : ReadRepository<AuthRolePage>, IAuthRolePageReadRepository
    {
        private readonly Emasist2024Context _context;
        public AuthRolePageReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}


