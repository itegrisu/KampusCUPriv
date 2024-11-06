using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleAllRepo
{
    public class VehicleAllWriteRepository : WriteRepository<VehicleAll>, IVehicleAllWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleAllWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
