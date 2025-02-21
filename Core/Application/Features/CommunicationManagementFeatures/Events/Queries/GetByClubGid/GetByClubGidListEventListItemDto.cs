using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommunicationManagementFeatures.Events.Queries.GetByClubGid
{
    public class GetByClubGidListEventListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidClubFK { get; set; }
        public string ClubFKName { get; set; }
        public string ClubFKColor { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public EnumEventStatus EventStatus { get; set; }
    }
}
