using Application.Repositories.GeneralManagementRepos.UserShortCutRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagementRepos.UserShortCutRepo
{
    public class UserShortCutWriteRepository : WriteRepository<UserShortCut>, IUserShortCutWriteRepository
    {
        private readonly Emasist2024Context _context;
        public UserShortCutWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
