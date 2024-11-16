using Application.Repositories.VehicleManagementsRepos.VehicleInsuranceRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleInsuranceRepo
{
    public class VehicleInsuranceReadRepository : ReadRepository<VehicleInsurance>, IVehicleInsuranceReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleInsuranceReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
