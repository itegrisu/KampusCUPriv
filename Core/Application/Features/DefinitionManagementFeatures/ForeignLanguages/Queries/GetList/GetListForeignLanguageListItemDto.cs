using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetList;

public class GetListForeignLanguageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string DilAdi { get; set; }
    public string? DilKodu { get; set; }


}