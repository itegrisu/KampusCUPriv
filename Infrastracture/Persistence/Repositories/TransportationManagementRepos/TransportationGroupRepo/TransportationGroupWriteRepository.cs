using Application.Repositories.TransportationRepos.TransportationGroupRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationGroupRepo
{
    public class TransportationGroupWriteRepository : WriteRepository<TransportationGroup>, ITransportationGroupWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationGroupWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
