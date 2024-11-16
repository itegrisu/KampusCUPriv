using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleInspections.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleInspectionListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}
