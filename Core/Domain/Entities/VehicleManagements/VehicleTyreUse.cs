using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleTyreUse : BaseEntity
    {
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public Guid GidTyreFK { get; set; }
        public Tyre TyreFK { get; set; }
        public DateTime InstallationDate { get; set; }
        public DateTime? TyreRemovalDate { get; set; }
    }
}
