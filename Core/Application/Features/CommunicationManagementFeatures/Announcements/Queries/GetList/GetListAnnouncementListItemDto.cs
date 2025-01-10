using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetList;

public class GetListAnnouncementListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidClubFK { get; set; }
    public string ClubFKName { get; set; }
    public Guid? GidUserFK { get; set; }
    public string UserFKName { get; set; }
    public Guid GidAnnouncementType { get; set; }
    public string AnnouncementTypeFKName { get; set; }
    public string Description { get; set; }
    public bool IsRead { get; set; }
}