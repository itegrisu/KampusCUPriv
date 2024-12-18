using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.TransportationManagements;
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
        public bool IsSubmitted { get; set; }
        public string? Description { get; set; }

        public ICollection<VehicleTransaction>? VehicleTransactions { get; set; }
        public ICollection<VehicleDocument>? VehicleDocuments { get; set; }
        public ICollection<VehicleInsurance>? VehicleInsurances { get; set; }
        public ICollection<VehicleMaintenance>? VehicleMaintenances { get; set; }
        public ICollection<VehicleInspection>? VehicleInspections { get; set; }
        public ICollection<VehicleEquipment>? VehicleEquipments { get; set; }
        public ICollection<VehicleFuel>? VehicleFuels { get; set; }
        public ICollection<VehicleTyreUse>? VehicleTyreUses { get; set; }
        public ICollection<VehicleRequest>? VehicleRequests { get; set; }
        public ICollection<TransportationService>? TransportationServices { get; set; }
        public ICollection<VehicleAccident>? VehicleAccidents { get; set; }
    }
}
