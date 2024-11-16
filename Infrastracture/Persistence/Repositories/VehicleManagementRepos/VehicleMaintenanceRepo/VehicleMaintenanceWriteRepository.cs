using Application.Repositories.VehicleManagementsRepos.VehicleMaintenanceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleMaintenanceRepo
{
    public class VehicleMaintenanceWriteRepository : WriteRepository<VehicleMaintenance>, IVehicleMaintenanceWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleMaintenanceWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
