using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleAccidentRepo
{
    public class VehicleAccidentWriteRepository : WriteRepository<VehicleAccident>, IVehicleAccidentWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleAccidentWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
