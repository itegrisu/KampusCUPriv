using Application.Repositories.VehicleManagementsRepos.VehicleInspectionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleInspectionRepo
{
    public class VehicleInspectionReadRepository : ReadRepository<VehicleInspection>, IVehicleInspectionReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleInspectionReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
