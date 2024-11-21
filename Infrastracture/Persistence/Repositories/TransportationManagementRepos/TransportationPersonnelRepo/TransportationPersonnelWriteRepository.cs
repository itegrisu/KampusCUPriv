using Application.Repositories.TransportationRepos.TransportationPersonnelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.TransportationManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.TransportationManagementRepos.TransportationPersonnelRepo
{
    public class TransportationPersonnelWriteRepository : WriteRepository<TransportationPersonnel>, ITransportationPersonnelWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TransportationPersonnelWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
