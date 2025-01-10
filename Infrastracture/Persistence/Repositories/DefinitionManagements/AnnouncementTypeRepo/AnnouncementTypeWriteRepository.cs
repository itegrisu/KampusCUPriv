using Application.Repositories.DefinitionManagementRepo.AnnouncementTypeRepo;
using Core.Repositories.Concretes;
using Domain.Entities.DefinitionManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DefinitionManagements.AnnouncementTypeRepo
{
    public class AnnouncementTypeWriteRepository : WriteRepository<AnnouncementType>, IAnnouncementTypeWriteRepository
    {
        private readonly KampusCUContext _context;
        public AnnouncementTypeWriteRepository(KampusCUContext context) : base(context)
        {
            _context = context;
        }
    }
}
