using Application.Repositories.TransportationRepos.TransportationPassengerRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationPassengerRepo
{
    public class TransportationPassengerWriteRepository : WriteRepository<TransportationPassenger>, ITransportationPassengerWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationPassengerWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
