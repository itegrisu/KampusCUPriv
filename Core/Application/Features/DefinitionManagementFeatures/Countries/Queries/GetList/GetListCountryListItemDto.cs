using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Countries.Queries.GetList;

public class GetListCountryListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string UlkeAdi { get; set; }
    public string UlkeKodu { get; set; }
    public string? TelefonKodu { get; set; }
    public int RowNo { get; set; }


}