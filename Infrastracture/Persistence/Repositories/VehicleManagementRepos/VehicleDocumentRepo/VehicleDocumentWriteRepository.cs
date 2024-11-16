using Application.Repositories.VehicleManagementsRepos.VehicleDocumentRepo;
using Core.Repositories.Concretes;
using Domain.Entities.VehicleManagements;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.VehicleManagementRepos.VehicleDocumentRepo
{
    public class VehicleDocumentWriteRepository : WriteRepository<VehicleDocument>, IVehicleDocumentWriteRepository
    {
        private readonly Emasist2024Context _context;
        public VehicleDocumentWriteRepository(Emasist2024Context context) : base(context)
        {
            _context = context;
        }
    }
}
