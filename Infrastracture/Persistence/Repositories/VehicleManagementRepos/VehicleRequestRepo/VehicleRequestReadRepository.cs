using Application.Repositories.VehicleManagementsRepos.VehicleRequestRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleRequestRepo
{
    public class VehicleRequestReadRepository : ReadRepository<VehicleRequest>, IVehicleRequestReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleRequestReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
