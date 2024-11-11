using Application.Repositories.VehicleManagementsRepos.VehicleTransactionRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleTransactionRepo
{
    public class VehicleTransactionReadRepository : ReadRepository<VehicleTransaction>, IVehicleTransactionReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleTransactionReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
