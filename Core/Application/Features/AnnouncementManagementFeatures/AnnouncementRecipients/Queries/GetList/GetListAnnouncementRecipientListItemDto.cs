using Core.Application.Dtos;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Queries.GetList;

public class GetListAnnouncementRecipientListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidAnnouncementFK { get; set; }
    public Guid GidRecipientFK { get; set; }
    public string UserFKFullName { get; set; }
    public string UserFKAvatar { get; set; }
    public DateTime? ReadDate { get; set; }
    public string? ReadIpAddress { get; set; }
    public bool? Confirm { get; set; }
}