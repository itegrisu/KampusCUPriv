using Core.Application.Dtos;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.NotificationManagementFeatures.Notifications.Queries.GetUnreadByUserGid
{
    public class GetUnreadByUserGidListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string Title { get; set; }
        public ProcessType ProcessType { get; set; }
        public DateTime? ReadingDate { get; set; }
        public string? ReadingIp { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
