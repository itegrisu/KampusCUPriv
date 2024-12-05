using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationPassenger : BaseEntity
    {
        public Guid GidTransportationGroupFK { get; set; }
        public TransportationGroup TransportationGroupFK { get; set; }
        public string Country { get; set; } = string.Empty;
        public string IdentityNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public EnumGender Gender { get; set; }
        public string? Phone { get; set; }
        public EnumPassengerStatus PassengerStatus { get; set; }
        public string? RefNoTransportationPassenger { get; set; }
    }
}
