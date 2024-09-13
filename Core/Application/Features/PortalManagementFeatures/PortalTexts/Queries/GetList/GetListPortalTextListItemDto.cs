using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetList;

public class GetListPortalTextListItemDto : IDto
{
    public Guid Gid { get; set; }

public string Title { get; set; }
public string? Content { get; set; }
public string? Description { get; set; }
public bool IsRichTextBox { get; set; }
public string? ContentRich { get; set; }


}