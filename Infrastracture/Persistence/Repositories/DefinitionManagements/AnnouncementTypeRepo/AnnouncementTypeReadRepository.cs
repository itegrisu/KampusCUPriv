using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;

namespace Persistence.Repositories.DefinitionManagements.AnnouncementTypeRepo
{
    public class AnnouncementTypeReadRepository : ReadRepository<AnnouncementType>, IAnnouncementTypeReadRepository
    {
        private readonly KampusCUContext _context;
        public AnnouncementTypeReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
