using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.GeneralManagements
{
    public class UserReminder : BaseEntity
    {

        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Document { get; set; }
        public EnumReminderType ReminderType { get; set; }

    }
}
