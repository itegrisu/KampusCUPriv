using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommunicationManagementFeatures.Announcements.Queries.GetByClubGid
{
    public class GetByClubGidListAnnouncementListItemDto : IDto
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
