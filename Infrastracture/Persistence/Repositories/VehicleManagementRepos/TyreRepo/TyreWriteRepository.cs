using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.TyreRepo
{
    public class TyreWriteRepository : WriteRepository<Tyre>, ITyreWriteRepository
    {
        private readonly Emasist2024Context _context;
        public TyreWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
