using Core.Application.Responses;
using Core.Enum;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetByGid;

public class GetByGidNotificationResponse : IResponse
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string Title { get; set; }
    public ProcessType ProcessType { get; set; }
    public DateTime? ReadingDate { get; set; }
    public string? ReadingIp { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
}