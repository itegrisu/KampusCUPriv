using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleDocumentRepo
{
    public class VehicleDocumentReadRepository : ReadRepository<VehicleDocument>, IVehicleDocumentReadRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleDocumentReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
