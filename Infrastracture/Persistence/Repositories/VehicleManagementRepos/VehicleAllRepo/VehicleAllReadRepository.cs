using Application.Repositories.VehicleManagementsRepos.VehicleAllRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleAllRepo
{
    public class VehicleAllReadRepository : ReadRepository<VehicleAll>, IVehicleAllReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleAllReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
