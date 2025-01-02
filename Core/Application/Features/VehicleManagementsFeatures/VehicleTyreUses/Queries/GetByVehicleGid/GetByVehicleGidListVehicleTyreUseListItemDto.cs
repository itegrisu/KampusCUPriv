using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleTyreUses.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleTyreUseListItemDto  :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public Guid GidTyreFK { get; set; }
        public string TyreFKTyreTypeFKTitle { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? TyreRemovalDate { get; set; }
    }
}
