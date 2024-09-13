using Application.Repositories.GeneralManagementRepos.UserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserRepo
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        private readonly Emasist2024Context _context;
        public UserReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
