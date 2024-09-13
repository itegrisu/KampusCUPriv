using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;

public class GetListCityListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUlkeFK { get; set; }
    public string CountryFKUlkeAdi { get; set; }
    public string SehirAdi { get; set; }
    public string? PlakaKodu { get; set; }
}