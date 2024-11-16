using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleTyreUseRepo
{
    public class VehicleTyreUseWriteRepository : WriteRepository<VehicleTyreUse>, IVehicleTyreUseWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleTyreUseWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
