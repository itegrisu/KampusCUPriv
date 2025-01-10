using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Queries.GetList;

public class GetListAnnouncementTypeListItemDto : IDto
{
    public Guid Gid { get; set; }
    public string Name { get; set; }
}