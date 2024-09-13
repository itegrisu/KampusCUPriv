using Core.Application.Responses;

namespace Application.Features.PortalManagementFeatures.PortalTexts.Queries.GetByGid
{
    public class GetByGidPortalTextResponse : IResponse
    {
        public Guid Gid { get; set; }

public string Title { get; set; }
public string? Content { get; set; }
public string? Description { get; set; }
public bool IsRichTextBox { get; set; }
public string? ContentRich { get; set; }

    }
}