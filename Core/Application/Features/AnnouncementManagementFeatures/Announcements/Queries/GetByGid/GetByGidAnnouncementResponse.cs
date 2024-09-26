using Core.Application.Responses;
using Core.Enum;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Queries.GetByGid;

public class GetByGidAnnouncementResponse : IResponse
{
    public Guid Gid { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? Link { get; set; }
    public string? Image { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public ShowType ShowType { get; set; }
    public int RowNo { get; set; }
}