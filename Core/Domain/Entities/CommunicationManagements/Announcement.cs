using Core.Entities;
using Domain.Entities.ClubManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CommunicationManagements
{
    public class Announcement : BaseEntity
    {
        public Guid? GidClubFK { get; set; }
        public Club? ClubFK { get; set; }
        public Guid GidAnnouncementType { get; set; }
        public EnumAnnouncementType? AnnouncementType { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<StudentAnnouncement>? StudentAnnouncements { get; set; }
    }
}
