using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleInsurance : BaseEntity
    {

        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public EnumInsuranceType InsuranceType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int InsuranceFee { get; set; }
        public string? DocumentFile { get; set; }
        public string? Description { get; set; }
    }
}
