using Application.Repositories.ClubManagementRepos.ClubRepo;
using Core.Repositories.Concretes;
using Domain.Entities.ClubManagements;
using Persistence.Context;

namespace Persistence.Repositories.ClubManagements.ClubRepo
{
    public class ClubReadRepository : ReadRepository<Club>, IClubReadRepository
    {
        private readonly KampusCUContext _context;
        public ClubReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
