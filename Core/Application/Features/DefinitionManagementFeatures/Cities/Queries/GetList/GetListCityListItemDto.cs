using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetList;

public class GetListCityListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidCountryFK { get; set; }
    public string CountryFKName { get; set; }
    public string Name { get; set; }
    public string? PlateCode { get; set; }
}