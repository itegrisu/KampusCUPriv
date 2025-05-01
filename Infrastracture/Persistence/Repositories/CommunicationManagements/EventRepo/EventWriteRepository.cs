using Application.Repositories.CommunicationManagementRepo.EventRepo;
using Core.Repositories.Concretes;
using Domain.Entities.CommunicationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.CommunicationManagements.EventRepo
{
    public class EventWriteRepository : WriteRepository<Event>, IEventWriteRepository
    {
        private readonly KampusCUContext _context;
        public EventWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
