using Application.Repositories.VehicleManagementsRepos.TyreRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;

namespace Persistence.Repositories.VehicleManagementRepos.TyreRepo
{
    public class TyreReadRepository : ReadRepository<Tyre>, ITyreReadRepository
    {
        private readonly Emasist2024Context _context;
        public TyreReadRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
