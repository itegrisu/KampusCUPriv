using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleInsuranceRepo
{
    public class VehicleInsuranceWriteRepository : WriteRepository<VehicleInsurance>, IVehicleInsuranceWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleInsuranceWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
