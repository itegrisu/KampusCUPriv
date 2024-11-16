using Application.Repositories.VehicleManagementsRepos.VehicleTyreUseRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleTyreUseRepo
{
    public class VehicleTyreUseReadRepository : ReadRepository<VehicleTyreUse>, IVehicleTyreUseReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleTyreUseReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
