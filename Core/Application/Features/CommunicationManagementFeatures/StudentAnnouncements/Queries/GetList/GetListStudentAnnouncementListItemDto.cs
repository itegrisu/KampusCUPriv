using Core.Application.Dtos;
using Core.Enum;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKName { get; set; }
    public Guid GidAnnouncementFK { get; set; }
    public string AnnouncementFKDescription { get; set; }
    public EnumAnnouncementType AnnouncementFKAnnouncementType { get; set; }
    public string AnnouncementFKClubFKLogo { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
}