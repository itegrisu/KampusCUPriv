using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Queries.GetList;

public class GetListCurrencyListItemDto : IDto
{

    public Guid Gid { get; set; }
    public string Name { get; set; }
    public string? Code { get; set; }
    public string? Symbol { get; set; }


}