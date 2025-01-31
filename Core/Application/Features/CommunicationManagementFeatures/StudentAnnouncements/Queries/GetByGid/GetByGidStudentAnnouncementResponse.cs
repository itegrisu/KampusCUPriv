using Core.Application.Responses;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Queries.GetByGid
{
    public class GetByGidStudentAnnouncementResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidAnnouncementFK { get; set; }
        public string AnnouncementFKDescription { get; set; }
        public string AnnouncementFKClubFKLogo { get; set; }
        public bool IsRead { get; set; }
    }
}