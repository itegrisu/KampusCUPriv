using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Queries.GetList;

public class GetListForeignLanguageListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
    public string? LanguageCode { get; set; }


}