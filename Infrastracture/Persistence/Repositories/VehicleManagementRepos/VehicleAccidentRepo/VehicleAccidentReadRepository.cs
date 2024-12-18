using Application.Repositories.VehicleManagementsRepos.VehicleAccidentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleAccidentRepo
{
    public class VehicleAccidentReadRepository : ReadRepository<VehicleAccident>, IVehicleAccidentReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleAccidentReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
