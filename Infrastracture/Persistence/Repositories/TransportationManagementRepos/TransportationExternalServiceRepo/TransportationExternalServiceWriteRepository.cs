using Application.Repositories.TransportationRepos.TransportationExternalServiceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationExternalServiceRepo
{
    public class TransportationExternalServiceWriteRepository : WriteRepository<TransportationExternalService>, ITransportationExternalServiceWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationExternalServiceWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
