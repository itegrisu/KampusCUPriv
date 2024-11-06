using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleAll : BaseEntity
    {
        public Guid GidVehicleBrand { get; set; }
        public OtoBrand OtoBrandFK { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public EnumVehicleType VehicleType { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? EngineNo { get; set; }
        public string? ChassisNumber { get; set; }
        public int PassengerCount { get; set; }
        public EnumFuelType FuelType { get; set; }
        public string? Description { get; set; }


    }
}
