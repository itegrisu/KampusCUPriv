using Core.Entities;
using Domain.Entities.VehicleManagements;
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
    }
}
