using Application.Repositories.CommunicationManagementRepo.CalendarRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;

namespace Persistence.Repositories.CommunicationManagements.CalendarRepo
{
    public class CalendarReadRepository : ReadRepository<Calendar>, ICalendarReadRepository
    {
        private readonly KampusCUContext _context;
        public CalendarReadRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
