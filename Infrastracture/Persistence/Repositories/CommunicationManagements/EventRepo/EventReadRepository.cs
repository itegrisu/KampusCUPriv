using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;

namespace Persistence.Repositories.CommunicationManagements.EventRepo
{
    public class EventReadRepository : ReadRepository<Event>, IEventReadRepository
    {
        private readonly KampusCUContext _context;
        public EventReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
