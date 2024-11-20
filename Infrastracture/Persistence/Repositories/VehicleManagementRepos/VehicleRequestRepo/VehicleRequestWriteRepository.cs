using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleRequestRepo
{
    public class VehicleRequestWriteRepository : WriteRepository<VehicleRequest>, IVehicleRequestWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleRequestWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
