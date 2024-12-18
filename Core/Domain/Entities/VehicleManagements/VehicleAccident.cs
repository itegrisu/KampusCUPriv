using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleAccident : BaseEntity
    {
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Driver { get; set; } = string.Empty;
        public string? AccidentFile { get; set; }
        public string? AccidentImageFile { get; set; }
    }
}
