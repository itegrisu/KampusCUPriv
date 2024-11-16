using Application.Repositories.VehicleManagementsRepos.VehicleFuelRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleFuelRepo
{
    public class VehicleFuelReadRepository : ReadRepository<VehicleFuel>, IVehicleFuelReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleFuelReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
