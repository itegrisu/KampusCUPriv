using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommunicationManagementFeatures.StudentAnnouncements.Queries.GetByUserGid
{
    public class GetByUserGidListStudentAnnouncementListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKName { get; set; }
        public Guid GidAnnouncementFK { get; set; }
        public string AnnouncementFKDescription { get; set; }
        public bool IsRead { get; set; }
    }
}
