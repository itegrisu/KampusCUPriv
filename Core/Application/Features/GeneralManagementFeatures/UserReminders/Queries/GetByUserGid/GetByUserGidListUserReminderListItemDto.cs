using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Queries.GetByUserGid
{
    public class GetByUserGidListUserReminderListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public string UserFKFullName { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Document { get; set; }
        public EnumReminderType ReminderType { get; set; }
    }
}
