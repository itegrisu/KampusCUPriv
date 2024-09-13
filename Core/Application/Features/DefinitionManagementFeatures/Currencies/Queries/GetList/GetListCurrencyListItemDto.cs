using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetList;

public class GetListCurrencyListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string DovizAdi { get; set; }
    public string? DovizKodu { get; set; }
    public string? DovizSimgesi { get; set; }


}