using Core.Application.Responses;
using Domain.Entities.DefinitionManagements;
using Domain.Enums;

namespace Application.Features.CommunicationFeatures.Announcements.Queries.GetByGid
{
    public class GetByGidAnnouncementResponse : IResponse
    {
        public Guid Gid { get; set; }
        public Guid? GidClubFK { get; set; }
        public string ClubFKName { get; set; }
        public string ClubFKLogo { get; set; }
        public EnumAnnouncementType? AnnouncementType { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}