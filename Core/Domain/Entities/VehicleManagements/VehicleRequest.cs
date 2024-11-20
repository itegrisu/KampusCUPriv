using Core.Entities;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.VehicleManagements
{
    public class VehicleRequest : BaseEntity
    {
        public Guid GidVehicleFK { get; set; }
        public VehicleAll VehicleAllFK { get; set; }
        public Guid GidRequestUserFK { get; set; }
        public User RequestUserFK { get; set; }
        public Guid? GidApprovedUserFK { get; set; }
        public User? ApprovedUserFK { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UseAim { get; set; } = string.Empty;
        public EnumVehicleApprovedStatus VehicleApprovedStatus { get; set; }
    }
}
