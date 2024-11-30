using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Queries.GetByGid
{
    public class GetByGidTransportationPassengerResponse : IResponse
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

    }
}