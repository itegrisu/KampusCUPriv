using Application.Repositories.TransportationRepos.TransportationServiceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationServiceRepo
{
    public class TransportationServiceWriteRepository : WriteRepository<TransportationService>, ITransportationServiceWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationServiceWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
