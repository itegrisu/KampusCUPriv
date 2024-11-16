using Application.Repositories.VehicleManagementsRepos.VehicleEquipmentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleEquipmentRepo
{
    public class VehicleEquipmentWriteRepository : WriteRepository<VehicleEquipment>, IVehicleEquipmentWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleEquipmentWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
