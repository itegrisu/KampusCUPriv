using Core.Application.Dtos;

namespace Application.Features.DefinitionManagementFeatures.DocumentTypes.Queries.GetList;

public class GetListDocumentTypeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string BelgeAdi { get; set; }


}