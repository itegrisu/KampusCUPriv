using Core.Application.Responses;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByGid
{
    public class GetByGidPersonnelAddressResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidCityFK { get; set; }
        public string CityFKName { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }

    }
}