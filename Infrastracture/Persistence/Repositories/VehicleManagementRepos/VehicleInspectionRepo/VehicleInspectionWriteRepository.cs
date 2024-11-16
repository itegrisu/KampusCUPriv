using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleInspectionRepo
{
    public class VehicleInspectionWriteRepository : WriteRepository<VehicleInspection>, IVehicleInspectionWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleInspectionWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
