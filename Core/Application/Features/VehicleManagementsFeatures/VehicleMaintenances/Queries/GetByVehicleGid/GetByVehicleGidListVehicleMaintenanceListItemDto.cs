using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleMaintenances.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleMaintenanceListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public string? ResponsiblePerson { get; set; }
        public int MaintenanceFee { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
        public int MaintenanceScore { get; set; }
    }
}
