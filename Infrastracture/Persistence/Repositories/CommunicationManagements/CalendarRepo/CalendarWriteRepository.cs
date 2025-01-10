using Application.Repositories.CommunicationManagementRepo.CalendarRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CommunicationManagements.CalendarRepo
{
    public class CalendarWriteRepository : WriteRepository<Calendar>, ICalendarWriteRepository
    {
        private readonly KampusCUContext _context;
        public CalendarWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
