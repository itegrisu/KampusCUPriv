using Core.Entities;
using Domain.Entities.VehicleManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationService : BaseEntity
    {
        public Guid GidTransportationFK { get; set; }
        public Transportation TransportationFK { get; set; }
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public string ServiceNo { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? StartKM { get; set; }
        public int? EndKM { get; set; }
        public string? VehiclePhone { get; set; }
        public EnumTransportationServiceStatus TransportationServiceStatus { get; set; }
        public string? TransportationFile { get; set; }
        public string? Description { get; set; }
        public string? RefNoTransportation { get; set; }
        public ICollection<TransportationGroup>? TransportationGroups { get; set; }
        public ICollection<TransportationPersonnel>? TransportationPersonnels { get; set; }
    }
}
