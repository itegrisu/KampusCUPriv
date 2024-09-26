using Core.Entities;
using Core.Enum;

namespace Domain.Entities.AnnouncementManagements
{
    public class Announcement : BaseEntity, IHasRowNo
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Link { get; set; }
        public string? Image { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public ShowType ShowType { get; set; }
        public int RowNo { get; set; }
        public ICollection<AnnouncementRecipient>? AnnouncementRecipients { get; set; }
    }
}