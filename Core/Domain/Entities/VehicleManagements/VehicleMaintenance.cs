using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleMaintenance : BaseEntity
    {
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? ResponsiblePerson { get; set; }
        public int MaintenanceFee { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
        public int? MaintenanceScore { get; set; }
    }
}
