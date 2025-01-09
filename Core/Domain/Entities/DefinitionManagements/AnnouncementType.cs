using Core.Entities;
using Domain.Entities.CommunicationManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class AnnouncementType : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<Announcement>? Announcements { get; set; }
    }
}
