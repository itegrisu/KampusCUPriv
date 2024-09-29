using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid
{
    public class GetByGidCityResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidCountryFK { get; set; }
        public string CountryFKName { get; set; }
        public string Name { get; set; }
        public string? PlateCode { get; set; }

    }
}