using Domain.Entities.AnnouncementManagements;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfiguration.AnnouncementManagements
{
    public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(t => t.Gid);
            builder.Property(y => y.Gid).IsRequired().HasColumnType("uniqueidentifier");

            builder.Property(y => y.Title).IsRequired().HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.Description).IsRequired().HasColumnType("varchar").HasMaxLength(1000);
            builder.Property(y => y.Link).IsRequired(false).HasColumnType("varchar").HasMaxLength(250);
            builder.Property(y => y.Image).IsRequired(false).HasColumnType("varchar").HasMaxLength(150);
            builder.Property(y => y.StartDate).IsRequired();
            builder.Property(y => y.EndDate).IsRequired();
            builder.Property(y => y.RowNo).IsRequired();

            builder.HasMany(u => u.AnnouncementRecipients).WithOne(y => y.Announcement).HasForeignKey(y => y.GidAnnouncementFK);
        }
    }
}