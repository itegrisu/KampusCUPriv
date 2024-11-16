using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.VehicleManagementsFeatures.VehicleInsurances.Queries.GetByVehicleGid
{
    public class GetByVehicleGidListVehicleInsuranceListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidVehicleFK { get; set; }
        public string VehicleAllFKPlateNumber { get; set; }
        public EnumInsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InsuranceFee { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}
