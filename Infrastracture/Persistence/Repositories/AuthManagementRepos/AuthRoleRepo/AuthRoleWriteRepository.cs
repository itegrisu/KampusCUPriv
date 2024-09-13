using Application.Repositories.AuthManagementRepos.AuthRoleRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthRoleRepo
{
    public class AuthRoleWriteRepository : WriteRepository<AuthRole>, IAuthRoleWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AuthRoleWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
