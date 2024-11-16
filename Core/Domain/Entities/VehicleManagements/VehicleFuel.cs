using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleFuel : BaseEntity
    {
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }

    }
}
