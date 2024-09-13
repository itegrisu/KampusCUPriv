using Application.Repositories.AuthManagementRepos.AuthRolePageRepo;
using Core.Repositories.Concretes;
using Domain.Entities.AuthManagements;
using Persistence.Context;

namespace Persistence.Repositories.AuthManagementRepos.AuthRolePageRepo
{
    public class AuthRolePageWriteRepository : WriteRepository<AuthRolePage>, IAuthRolePageWriteRepository
    {
        private readonly Emasist2024Context _context;
        public AuthRolePageWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}