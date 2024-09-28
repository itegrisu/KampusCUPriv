using Core.Application.Responses;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Queries.GetByGid
{
    public class GetByGidSCAddressResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidCityFK { get; set; }
        public string CityFKSehirAdi { get; set; }
        public string Title { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string Address { get; set; }

    }
}