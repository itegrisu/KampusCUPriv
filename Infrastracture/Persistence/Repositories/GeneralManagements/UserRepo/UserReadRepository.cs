using Application.Repositories.GeneralManagementRepo.UserRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagements.UserRepo
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        private readonly KampusCUContext _context;
        public UserReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
