using Core.Entities;
using Domain.Entities.ClubManagements;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.CommunicationManagements
{
    public class Event : BaseEntity
    {
        public Guid GidClubFK { get; set; }
        public Club ClubFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public EnumEventStatus EventStatus { get; set; }
    }
}
