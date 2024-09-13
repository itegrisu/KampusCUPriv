using Application.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserRefreshTokenRepo
{
    public class UserRefreshTokenReadRepository : ReadRepository<UserRefreshToken>, IUserRefreshTokenReadRepository
    {
        private readonly Emasist2024Context _context;
        public UserRefreshTokenReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
