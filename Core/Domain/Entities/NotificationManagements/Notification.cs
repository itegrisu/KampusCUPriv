using Core.Entities;
using Core.Enum;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.NotificationManagements
{
    public class Notification : BaseEntity
    {
        public Guid GidUserFK { get; set; }
        public User UserFK { get; set; }
        public string Title { get; set; } = string.Empty;
        public ProcessType ProcessType { get; set; }
        public DateTime? ReadingDate { get; set; }
        public string? ReadingIp { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}