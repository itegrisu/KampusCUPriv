using Core.Entities;
using Domain.Entities.CommunicationManagements;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ClubManagements
{
    public class Club : BaseEntity
    {
        public Guid? GidManagerFK { get; set; }
        public User? UserFK { get; set; }
        public Guid GidCategoryFK { get; set; }
        public Category CategoryFK { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }

        public ICollection<Event>? Events { get; set; }
        public ICollection<Announcement>? Announcements { get; set; }
        public ICollection<StudentClub>? StudentClubs { get; set; }
        public ICollection<Admin>? Admins { get; set; }
    }
}
