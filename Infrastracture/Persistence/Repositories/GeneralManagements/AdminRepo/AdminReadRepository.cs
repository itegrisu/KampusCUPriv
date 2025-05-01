using Application.Repositories.GeneralManagementRepo.AdminRepo;
using Core.Repositories.Concretes;
using Domain.Entities.GeneralManagements;
using Persistence.Context;

namespace Persistence.Repositories.GeneralManagements.AdminRepo
{
    public class AdminReadRepository : ReadRepository<Admin>, IAdminReadRepository
    {
        private readonly KampusCUContext _context;
        public AdminReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
