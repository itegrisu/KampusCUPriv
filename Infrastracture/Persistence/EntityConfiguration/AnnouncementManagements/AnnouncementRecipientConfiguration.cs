using Domain.Entities.AnnouncementManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.AnnouncementManagements
{
    public class AnnouncementRecipientConfiguration : IEntityTypeConfiguration<AnnouncementRecipient>
    {
        public void Configure(EntityTypeBuilder<AnnouncementRecipient> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.HasOne(y => y.Announcement).WithMany(u => u.AnnouncementRecipients).HasForeignKey(y => y.GidAnnouncementFK);
            builder.HasOne(y => y.UserFK).WithMany(u => u.AnnouncementRecipients).HasForeignKey(y => y.GidRecipientFK);

           
            builder.Property(y => y.ReadDate).IsRequired(false);
            builder.Property(y => y.ReadIpAddress).IsRequired(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(y => y.Confirm).IsRequired(false);
        }
    }
}