using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TransportationManagementFeatures.Transportations.Queries.GetByGid
{
    public class GetByGidTransportationResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid? GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public string CustomerInfo { get; set; }
        public string TransportationNo { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Fee { get; set; }
        public EnumTransportationStatus TransportationStatus { get; set; }

    }
}