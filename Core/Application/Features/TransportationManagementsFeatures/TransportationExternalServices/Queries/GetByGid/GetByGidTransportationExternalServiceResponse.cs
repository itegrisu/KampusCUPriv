using Core.Application.Responses;
using Domain.Enums;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Queries.GetByGid
{
    public class GetByGidTransportationExternalServiceResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidSupplierFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid? GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public Guid GidFeeCurrencyFK { get; set; }
        public string CurrencyFKName { get; set; }
        public string Title { get; set; }
        public decimal Fee { get; set; }
        public string PlateNo { get; set; }
        public EnumExternalVehicleType ExternalVehicleType { get; set; }
        public int PassengerCapacity { get; set; }
        public string? VehicleOfficer { get; set; }
        public string? VehiclePhone { get; set; }
        public bool IsHasFile { get; set; }
        public EnumExternalServiceStatus ExternalServiceStatus { get; set; }
        public string? Description { get; set; }

    }
}