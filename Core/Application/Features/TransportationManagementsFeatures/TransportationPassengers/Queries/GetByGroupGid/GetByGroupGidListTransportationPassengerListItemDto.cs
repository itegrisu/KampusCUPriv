using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationPassengers.Queries.GetByGroupGid
{
    public class GetByGroupGidListTransportationPassengerListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationGroupFK { get; set; }
        public string TransportationGroupFKGroupName { get; set; }
        public string Country { get; set; }
        public string IdentityNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EnumGender Gender { get; set; }
        public string? Phone { get; set; }
        public EnumPassengerStatus PassengerStatus { get; set; }
        public string? RefNoTransportationPassenger { get; set; }

    }
}
