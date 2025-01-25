using Core.Application.Dtos;
using Core.Enum;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementListItemDto : IDto
{
    public Guid Gid { get; set; }
    public Guid GidUserFK { get; set; }
    public string UserFKName { get; set; }
    public Guid GidAnnouncementFK { get; set; }
    public string AnnouncementFKDescription { get; set; }
    public bool IsRead { get; set; }
}