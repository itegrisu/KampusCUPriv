using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Queries.GetTaskCountList
{
    public class GetTaskCountListTaskUserListItemDto :IDto
    {
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public int ActiveTaskCount { get; set; }
        public int DelayedTaskCount { get; set; }
        public int CompletedTaskCount { get; set; }
    }
}
