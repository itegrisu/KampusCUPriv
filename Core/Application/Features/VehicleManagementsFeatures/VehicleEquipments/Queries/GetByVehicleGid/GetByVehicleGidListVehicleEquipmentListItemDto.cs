using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleEquipments.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleEquipmentListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public string EquipmentName { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}
