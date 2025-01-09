using Core.Entities;
using Domain.Entities.ClubManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
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
        public Guid? GidUserFK { get; set; }
        public User? UserFK { get; set; }
        public Guid GidAnnouncementType { get; set; }
        public AnnouncementType AnnouncementTypeFK { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
