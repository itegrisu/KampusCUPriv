using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetList;

public class GetListAnnouncementListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid? GidClubFK { get; set; }
    public string ClubFKName { get; set; }
    public string ClubFKLogo { get; set; }
    public EnumAnnouncementType? AnnouncementType { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
}