using Application.Repositories.GeneralManagementRepos.UserRefreshTokenRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserRefreshTokenRepo
{
    public class UserRefreshTokenWriteRepository : WriteRepository<UserRefreshToken>, IUserRefreshTokenWriteRepository
    {
        private readonly Emasist2024Context _context;
        public UserRefreshTokenWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
