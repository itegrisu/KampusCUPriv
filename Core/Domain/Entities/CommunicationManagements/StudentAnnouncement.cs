using Core.Entities;
using Domain.Entities.GeneralManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CommunicationManagements
{
    public class StudentAnnouncement : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidAnnouncementFK { get; set; }
        public Announcement AnnouncementFK { get; set; }
        public bool IsRead { get; set; }
    }
}
