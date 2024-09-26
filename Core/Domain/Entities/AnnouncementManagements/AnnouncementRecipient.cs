using Core.Entities;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.AnnouncementManagements
{
    public class AnnouncementRecipient : BaseEntity
    {
        public Guid GidAnnouncementFK { get; set; }
        public Announcement Announcement { get; set; }
        public Guid GidRecipientFK { get; set; }
        public User UserFK { get; set; }
        public DateTime? ReadDate { get; set; }
        public string? ReadIpAddress { get; set; }
        public bool? Confirm { get; set; }
    }
}