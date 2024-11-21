using Application.Repositories.TransportationRepos.TransportationRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationRepo
{
    public class TransportationWriteRepository : WriteRepository<Transportation>, ITransportationWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
