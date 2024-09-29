using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetList;

public class GetListCountryListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public string? PhoneCode { get; set; }
    public int RowNo { get; set; }

}