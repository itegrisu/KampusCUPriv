using Application.Repositories.GeneralManagementRepos.UserModuleAuthRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserModuleAuthRepo
{

    public class UserModuleAuthReadRepository : ReadRepository<UserModuleAuth>, IUserModuleAuthReadRepository
    {
        private readonly Emasist2024Context _context;
        public UserModuleAuthReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
