using Core.Application.Responses;
using Domain.Entities.DefinitionManagements;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid
{
    public class GetByGidAnnouncementResponse : IResponse
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
}