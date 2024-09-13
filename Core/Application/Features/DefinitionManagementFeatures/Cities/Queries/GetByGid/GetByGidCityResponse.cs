using Core.Application.Responses;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByGid
{
    public class GetByGidCityResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUlkeFK { get; set; }
        public string CountryFKUlkeAdi { get; set; }
        public string SehirAdi { get; set; }
        public string? PlakaKodu { get; set; }

    }
}